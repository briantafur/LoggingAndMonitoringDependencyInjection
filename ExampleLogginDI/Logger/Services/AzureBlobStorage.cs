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
        CloudBlobContainer Container;

        public AzureBlobStorage(String storageAccountName, String azureKey, String containerName)
        {
            InitializeContainer(storageAccountName, azureKey, containerName);
        }

        #region Configuration

        public async void InitializeContainer(String StorageAccountName, String AzureKey, String ContainerName)
        {
            //var StorageAccountName = "yuxiresearch";
            //var AzureKey = "fjUQhzRIbeJw0v6Qph6iaqZw92qMOL4MPzVQQqrx3hmvGPWnQGZQIFhL9SPjF4J5mF1Ppese2YqM2Agsp4YhVA==";
            //var ContainerName = "containerlog";
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
            var newBlob = Container.GetBlockBlobReference("log-" + date + ".txt");
            String oldContent = "";
            if (await newBlob.ExistsAsync())
            {
                using (StreamReader reader = new StreamReader(await newBlob.OpenReadAsync()))
                {
                    oldContent = await reader.ReadToEndAsync();
                }
            }
            else
            {
                MemoryStream file = new MemoryStream();
                await newBlob.UploadFromStreamAsync(file);
            }
            using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
            {
                await writer.WriteAsync(oldContent);
                await writer.WriteAsync("\n[Information]" + component.ToString() + " " + methodName + ": " + message);
            }
        }

        public async Task DebugAsyncFacade(string message, Type component, [CallerMemberName] string methodName = "")
        {
            String date = DateTime.Now.ToString("yyyyMMdd");
            var newBlob = Container.GetBlockBlobReference("log-" + date + ".txt");
            String oldContent = "";
            if (await newBlob.ExistsAsync())
            {
                using (StreamReader reader = new StreamReader(await newBlob.OpenReadAsync()))
                {
                    oldContent = await reader.ReadToEndAsync();
                }
            }
            else
            {
                MemoryStream file = new MemoryStream();
                await newBlob.UploadFromStreamAsync(file);
            }
            using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
            {
                await writer.WriteAsync(oldContent);
                await writer.WriteAsync("\n[Debug]" + component.ToString() + " " + methodName + ": " + message);
            }
        }

        public async Task ErrorAsyncFacade(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            String date = DateTime.Now.ToString("yyyyMMdd");
            var newBlob = Container.GetBlockBlobReference("log-" + date + ".txt");
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
            using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
            {
                await writer.WriteAsync(oldContent);
                await writer.WriteAsync("\n[Error]" + component.ToString() + " " + methodName + ": " + exception.Message + "\n" + exception.StackTrace);
            }
        }

        public async Task WarningAsyncFacade(string message, Type component, [CallerMemberName] string methodName = "")
        {
            String date = DateTime.Now.ToString("yyyyMMdd");
            var newBlob = Container.GetBlockBlobReference("log-" + date + ".txt");
            String oldContent = "";
            if (await newBlob.ExistsAsync())
            {
                using (StreamReader reader = new StreamReader(await newBlob.OpenReadAsync()))
                {
                    oldContent = await reader.ReadToEndAsync();
                }
            }
            else
            {
                MemoryStream file = new MemoryStream();
                await newBlob.UploadFromStreamAsync(file);
            }
            using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
            {
                await writer.WriteAsync(oldContent);
                await writer.WriteAsync("\n[Warning]" + component.ToString() + " " + methodName + ": " + message);
            }
        }

        public async Task FatalAsyncFacade(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            String date = DateTime.Now.ToString("yyyyMMdd");
            var newBlob = Container.GetBlockBlobReference("log-" + date + ".txt");
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
            using (StreamWriter writer = new StreamWriter(await newBlob.OpenWriteAsync()))
            {
                await writer.WriteAsync(oldContent);
                await writer.WriteAsync("\n[Fatal]" + component.ToString() + " " + methodName + ": " + exception.Message + "\n" + exception.StackTrace);
            }
        }

        #endregion

        #region Async Methods
        public async Task InfoAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            InfoAsyncFacade(message, component, methodName).Wait();
        }

        public async Task DebugAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            DebugAsyncFacade(message, component, methodName).Wait();
        }

        public async Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            ErrorAsyncFacade(exception, component, methodName).Wait();
        }

        public async Task WarningAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            WarningAsyncFacade(message, component, methodName).Wait();
        }

        public async Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            FatalAsyncFacade(exception, component, methodName).Wait();
        }
        #endregion

    }
}
