using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;

namespace Logger.Services
{
    class AmazonS3Storage : ILoggerInterface
    {
        public void Debug(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task DebugAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Info(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task InfoAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            /*    GetObjectRequest getObjRequest = new GetObjectRequest()
        .WithBucketName(amazonSettings.BucketName)
        .WithKey(_fileKey);*/

            /*GetObjectRequest nada = new GetObjectRequest();
            using (AmazonS3 client = AWSClientFactory.CreateAmazonS3Client(
                amazonSettings.AccessKey,
                amazonSettings.SecretAccessKey))
            using (GetObjectResponse getObjRespone = client.GetObject(getObjRequest))
            using (Stream amazonStream = getObjRespone.ResponseStream)
            {
                StreamReader amazonStreamReader = new StreamReader(amazonStream);
                tempGsContact = new GSContact();
                while ((_fileLine = amazonStreamReader.ReadLine()) != null)
                {
                    if (_fileLine.Equals("END:VCARD"))
                    {
                        // Make process 1
                    }
                    else if (!_fileLine.Equals(string.Empty))
                    {
                        //Make process 2
                    }
                }
            }*/
            throw new NotImplementedException();
        }

        public void Warning(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task WarningAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

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
