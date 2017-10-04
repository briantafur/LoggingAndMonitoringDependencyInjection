using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using System.Threading;
using System.IO;

namespace Logger.Services
{
    public class AmazonS3Storage : ILoggerInterface
    {

        string bucketName = "yuxiglobal-test-logfiles";
        //string keyName = "*** key name when object is created ***";
        //string filePath = "*** absolute path to a sample file to upload ***";
        IAmazonS3 client;


        public AmazonS3Storage()
        {

        }

        #region methods

        public void Info(string message, Type component, [CallerMemberName] string methodName = "")
        {




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

        #region asyncmethods

        public async Task InfoAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            Thread t = new Thread(() =>
            {
                Monitor.Enter(this);
                try
                {
                    InfoAsyncNada(message, component, methodName).Wait();
                }
                finally
                {
                    Monitor.Exit(this);
                }
            });
            t.Start();
        }

        public async Task InfoAsyncNada(string message, Type component, [CallerMemberName] string methodName = "")
        {
            String date = DateTime.Now.ToString("yyyyMMdd");
            String hour = DateTime.Now.ToString("HH:mm:ss");
            string keyName = "Infolog-" + date + ".txt";
            client = client = new AmazonS3Client("AKIAIBCOZRNVGWYWAHKQ", "vOCWeBYAz1Upj3mqDfhEpffYPV1TobZMekQJTLmZ", Amazon.RegionEndpoint.USEast1);


            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = keyName
            };
            PutObjectRequest putRequest1;
            try
            {
                string responseBody = "";
                using (GetObjectResponse response = await client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    responseBody = reader.ReadToEnd();
                }
                putRequest1 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    ContentBody = responseBody + "\n" + hour + "[Information]" + component.ToString() + " " + methodName + ": " + message
                };
            }
            catch (AmazonS3Exception e)
            {
                putRequest1 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    ContentBody = "\n" + hour + "[Information]" + component.ToString() + " " + methodName + ": " + message
                };
            }
            await client.PutObjectAsync(putRequest1);
        }

        public Task DebugAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task WarningAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        #endregion

        /*static void WritingAnObject()
        {
            try
            {
                PutObjectRequest putRequest1 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    ContentBody = "sample text"
                };

                PutObjectResponse response1 = client.PutObject(putRequest1);

                // 2. Put object-set ContentType and add metadata.
                PutObjectRequest putRequest2 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath,
                    ContentType = "text/plain"
                };
                putRequest2.Metadata.Add("x-amz-meta-title", "someTitle");

                PutObjectResponse response2 = client.PutObject(putRequest2);

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                        "For service sign up go to http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine(
                        "Error occurred. Message:'{0}' when writing an object"
                        , amazonS3Exception.Message);
                }
            }
        }*/



    }
}
