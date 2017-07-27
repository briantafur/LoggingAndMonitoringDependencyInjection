using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logger.Services
{
    public class AzureBlobStorage : ILoggerInterface
    {
        EventWaitHandle clearCount = new EventWaitHandle(false, EventResetMode.ManualReset);
        CloudBlobContainer Container;
        readonly float FileSize;

        public AzureBlobStorage(String storageAccountName, String azureKey, String containerName, float fileSize)
        {
            this.FileSize = fileSize;
            InitializeContainer(storageAccountName, azureKey, containerName);
        }

        #region Configuration

        public async void InitializeContainer(String StorageAccountName, String AzureKey, String ContainerName)
        {
            var storageCredentials = new StorageCredentials(StorageAccountName, AzureKey);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            Container = cloudBlobClient.GetContainerReference(ContainerName);
            await Container.CreateIfNotExistsAsync();
            await Container.SetPermissionsAsync(
               new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
        }

        #endregion

        #region Methods

        public void Info(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Warning(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Async Facade Methods

        public async Task InfoAsyncFacade(string message, Type component, [CallerMemberName] string methodName = "")
        {
            String date = DateTime.Now.ToString("yyyyMMdd");
            String hour = DateTime.Now.ToString("HH:mm:ss");
            CloudBlockBlob newBlob = Container.GetBlockBlobReference("Infolog-" + date + "0.txt");
            bool response = await newBlob.ExistsAsync();
            if (!response)
            {
                MemoryStream file = new MemoryStream();
                await newBlob.UploadFromStreamAsync(file);
            }
            var count = 0;
            do
            {
                newBlob = Container.GetBlockBlobReference("Infolog-" + date + count + ".txt");
                if (await newBlob.ExistsAsync())
                {
                    await newBlob.FetchAttributesAsync();
                    if (newBlob.Properties.Length < (FileSize * 1024))
                    {
                        String oldContent = await LectorAzureBlob(newBlob);
                        using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
                        {
                            await writer.WriteAsync(oldContent);
                            await writer.WriteAsync("\n" + hour + "[Information]" + component.ToString() + " " + methodName + ": " + message);
                            return;
                        }
                    }
                }
                count++;
            } while (await newBlob.ExistsAsync());
            MemoryStream newFile = new MemoryStream();
            await newBlob.UploadFromStreamAsync(newFile);
            using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
            {
                await writer.WriteAsync("\n" + hour + "[Information]" + component.ToString() + " " + methodName + ": " + message);
            }
        }

        public async Task DebugAsyncFacade(string message, Type component, [CallerMemberName] string methodName = "")
        {
            String date = DateTime.Now.ToString("yyyyMMdd");
            String hour = DateTime.Now.ToString("HH:mm:ss");
            CloudBlockBlob newBlob = Container.GetBlockBlobReference("Debuglog-" + date + "0.txt");
            bool response = await newBlob.ExistsAsync();
            if (!response)
            {
                MemoryStream file = new MemoryStream();
                await newBlob.UploadFromStreamAsync(file);
            }
            var count = 0;
            do
            {
                newBlob = Container.GetBlockBlobReference("Debuglog-" + date + count + ".txt");
                if (await newBlob.ExistsAsync())
                {
                    await newBlob.FetchAttributesAsync();
                    if (newBlob.Properties.Length < (FileSize * 1024))
                    {
                        String oldContent = await LectorAzureBlob(newBlob);
                        using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
                        {
                            await writer.WriteAsync(oldContent);
                            await writer.WriteAsync("\n" + hour + "[Debug]" + component.ToString() + " " + methodName + ": " + message);
                            return;
                        }
                    }
                }
                count++;
            } while (await newBlob.ExistsAsync());
            MemoryStream newFile = new MemoryStream();
            await newBlob.UploadFromStreamAsync(newFile);
            using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
            {
                await writer.WriteAsync("\n" + hour + "[Debug]" + component.ToString() + " " + methodName + ": " + message);
            }
        }

        public async Task ErrorAsyncFacade(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            String date = DateTime.Now.ToString("yyyyMMdd");
            String hour = DateTime.Now.ToString("HH:mm:ss");
            CloudBlockBlob newBlob = Container.GetBlockBlobReference("Errorlog-" + date + "0.txt");
            bool response = await newBlob.ExistsAsync();
            if (!response)
            {
                MemoryStream file = new MemoryStream();
                await newBlob.UploadFromStreamAsync(file);
            }
            var count = 0;
            do
            {
                newBlob = Container.GetBlockBlobReference("Errorlog-" + date + count + ".txt");
                if (await newBlob.ExistsAsync())
                {
                    await newBlob.FetchAttributesAsync();
                    if (newBlob.Properties.Length < (FileSize * 1024))
                    {
                        String oldContent = await LectorAzureBlob(newBlob);
                        using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
                        {
                            await writer.WriteAsync(oldContent);
                            await writer.WriteAsync("\n" + hour + "[Error]" + component.ToString() + " " + methodName + ": " + exception.Message + "\n" + exception.StackTrace);
                            return;
                        }
                    }
                }
                count++;
            } while (await newBlob.ExistsAsync());
            MemoryStream newFile = new MemoryStream();
            await newBlob.UploadFromStreamAsync(newFile);
            using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
            {
                await writer.WriteAsync("\n" + hour + "[Error]" + component.ToString() + " " + methodName + ": " + exception.Message + "\n" + exception.StackTrace);
            }
        }

        public async Task WarningAsyncFacade(string message, Type component, [CallerMemberName] string methodName = "")
        {
            String date = DateTime.Now.ToString("yyyyMMdd");
            String hour = DateTime.Now.ToString("HH:mm:ss");
            CloudBlockBlob newBlob = Container.GetBlockBlobReference("Warninglog-" + date + "0.txt");
            bool response = await newBlob.ExistsAsync();
            if (!response)
            {
                MemoryStream file = new MemoryStream();
                await newBlob.UploadFromStreamAsync(file);
            }
            var count = 0;
            do
            {
                newBlob = Container.GetBlockBlobReference("Warninglog-" + date + count + ".txt");
                if (await newBlob.ExistsAsync())
                {
                    await newBlob.FetchAttributesAsync();
                    if (newBlob.Properties.Length < (FileSize * 1024))
                    {
                        String oldContent = await LectorAzureBlob(newBlob);
                        using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
                        {
                            await writer.WriteAsync(oldContent);
                            await writer.WriteAsync("\n" + hour + "[Warning]" + component.ToString() + " " + methodName + ": " + message);
                            return;
                        }
                    }
                }
                count++;
            } while (await newBlob.ExistsAsync());
            MemoryStream newFile = new MemoryStream();
            await newBlob.UploadFromStreamAsync(newFile);
            using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
            {
                await writer.WriteAsync("\n" + hour + "[Warning]" + component.ToString() + " " + methodName + ": " + message);
            }
        }

        public async Task FatalAsyncFacade(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            String date = DateTime.Now.ToString("yyyyMMdd");
            String hour = DateTime.Now.ToString("HH:mm:ss");
            CloudBlockBlob newBlob = Container.GetBlockBlobReference("Fatallog-" + date + "0.txt");
            bool response = await newBlob.ExistsAsync();
            if (!response)
            {
                MemoryStream file = new MemoryStream();
                await newBlob.UploadFromStreamAsync(file);
            }
            var count = 0;
            do
            {
                newBlob = Container.GetBlockBlobReference("Fatallog-" + date + count + ".txt");
                if (await newBlob.ExistsAsync())
                {
                    await newBlob.FetchAttributesAsync();
                    if (newBlob.Properties.Length < (FileSize * 1024))
                    {
                        String oldContent = await LectorAzureBlob(newBlob);
                        using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
                        {
                            await writer.WriteAsync(oldContent);
                            await writer.WriteAsync("\n" + hour + "[Fatal]" + component.ToString() + " " + methodName + ": " + exception.Message + "\n" + exception.StackTrace);
                            return;
                        }
                    }
                }
                count++;
            } while (await newBlob.ExistsAsync());
            MemoryStream newFile = new MemoryStream();
            await newBlob.UploadFromStreamAsync(newFile);
            using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
            {
                await writer.WriteAsync("\n" + hour + "[Fatal]" + component.ToString() + " " + methodName + ": " + exception.Message + "\n" + exception.StackTrace);
            }
        }

        #endregion

        private async Task<String> LectorAzureBlob(CloudBlockBlob newBlob)
        {
            String oldContent = "";
            if (await newBlob.ExistsAsync())
            {
                using (StreamReader reader = new StreamReader(await newBlob.OpenReadAsync()))
                {
                    oldContent = reader.ReadToEnd();
                }
            }
            else
            {
                MemoryStream file = new MemoryStream();
                await newBlob.UploadFromStreamAsync(file);
            }
            return oldContent;
        }

        #region Async Methods
        public async Task InfoAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            Thread t = new Thread(() =>
            {
                Monitor.Enter(this);
                try
                {
                    InfoAsyncFacade(message, component, methodName).Wait();
                }
                finally
                {
                    Monitor.Exit(this);
                }
            });
            t.Start();
        }

        public async Task DebugAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            Thread t = new Thread(() =>
            {
                Monitor.Enter(this);
                try
                {
                    DebugAsyncFacade(message, component, methodName).Wait();
                }
                finally
                {
                    Monitor.Exit(this);
                }
            });
            t.Start();
        }

        public async Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            Thread t = new Thread(() =>
            {
                Monitor.Enter(this);
                try
                {
                    ErrorAsyncFacade(exception, component, methodName).Wait();
                }
                finally
                {
                    Monitor.Exit(this);
                }
            });
            t.Start();
        }

        public async Task WarningAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            Thread t = new Thread(() =>
            {
                Monitor.Enter(this);
                try
                {
                    WarningAsyncFacade(message, component, methodName).Wait();
                }
                finally
                {
                    Monitor.Exit(this);
                }
            });
            t.Start();
        }

        public async Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            Thread t = new Thread(() =>
            {
                Monitor.Enter(this);
                try
                {
                    FatalAsyncFacade(exception, component, methodName).Wait();
                }
                finally
                {
                    Monitor.Exit(this);
                }
            });
            t.Start();
        }
        #endregion

    }
}
