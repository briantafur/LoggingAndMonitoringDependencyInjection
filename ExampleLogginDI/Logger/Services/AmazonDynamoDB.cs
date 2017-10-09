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
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;


namespace Logger.Services
{
    public class AmazonS3Storage : ILoggerInterface
    {

        string bucketName = "yuxiglobal-test-logfiles";
        string keyName = "";
        string secretKey = "";
        IAmazonS3 client;


        public AmazonS3Storage(String key, String secretKey)
        {
            this.keyName = key;
            this.secretKey = secretKey;
        }

        private void CreateTable()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient("AKIAIBCOZRNVGWYWAHKQ", "vOCWeBYAz1Upj3mqDfhEpffYPV1TobZMekQJTLmZ", Amazon.RegionEndpoint.USEast2);

            CreateTableRequest createRequest = new CreateTableRequest
            {
                TableName = "Logs",
                AttributeDefinitions = new List<AttributeDefinition>()
             {
                 new AttributeDefinition
                 {
                     AttributeName = "Fecha",
                     AttributeType = "S"
                 },
                 new AttributeDefinition
                 {
                     AttributeName = "Hora",
                     AttributeType = "S"
                 }
             },
                KeySchema = new List<KeySchemaElement>()
             {
                 new KeySchemaElement
                 {
                     AttributeName = "Fecha",
                     KeyType = "HASH"
                 },
                 new KeySchemaElement
                 {
                     AttributeName = "Hora",
                     KeyType = "RANGE"
                 }
             },
            };
            createRequest.ProvisionedThroughput = new ProvisionedThroughput(1, 1);

            client.CreateTableAsync(createRequest).Wait();
        }


        private async Task DBRegister(String type, String component, String methodName, String message)
        {
            using (AmazonDynamoDBClient client = new AmazonDynamoDBClient(keyName, secretKey, Amazon.RegionEndpoint.USEast2))
            {

                String date = DateTime.Now.ToString("yyyy-MM-dd");
                //String hour = DateTime.Now.ToString("HH:mm:ss");
                String hour = DateTime.Now.ToString("HH:mm:ss.ffff");

                var request = new PutItemRequest
                {
                    TableName = "Logs",
                    Item = new Dictionary<string, AttributeValue>()
            {
                { "Fecha", new AttributeValue {
                      S = date
                  }},
                { "Hora", new AttributeValue {
                      S = hour
                  }},
                { "Tipo", new AttributeValue {
                      S = type
                  }},
                { "Componente", new AttributeValue {
                      S = component
                  }},
                { "Metodo", new AttributeValue {
                      S = methodName
                  }},
                { "Mensaje", new AttributeValue {
                      S = message
                  }}
            }
                };
                await client.PutItemAsync(request);
            }
        }

        #region methods

        public void Info(string message, Type component, [CallerMemberName] string methodName = "")
        {
            DBRegister("Information", component.ToString(), methodName, message).Wait();
        }

        public void Debug(string message, Type component, [CallerMemberName] string methodName = "")
        {
            DBRegister("Debug", component.ToString(), methodName, message).Wait();
        }

        public void Error(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            DBRegister("Error", component.ToString(), methodName, exception.Message).Wait();
        }

        public void Warning(string message, Type component, [CallerMemberName] string methodName = "")
        {
            DBRegister("Warning", component.ToString(), methodName, message).Wait();
        }

        public void Fatal(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            DBRegister("Fatal", component.ToString(), methodName, exception.Message).Wait();
        }

        #endregion

        #region asyncmethods

        public async Task InfoAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            /*Thread t = new Thread(() =>
            {
                Monitor.Enter(this);
                try
                {
                    DBRegister("Information", component.ToString(), methodName, message);
                }
                finally
                {
                    Monitor.Exit(this);
                }
            });
            t.Start();*/
            await DBRegister("Information", component.ToString(), methodName, message);
        }

        public async Task DebugAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            /*Thread t = new Thread(() =>
            {
                Monitor.Enter(this);
                try
                {
                    DBRegister("Debug", component.ToString(), methodName, message);
                }
                finally
                {
                    Monitor.Exit(this);
                }
            });
            t.Start();*/
            await DBRegister("Debug", component.ToString(), methodName, message);
        }

        public async Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            /*Thread t = new Thread(() =>
            {
                Monitor.Enter(this);
                try
                {
                    DBRegister("Error", component.ToString(), methodName, exception.Message);
                }
                finally
                {
                    Monitor.Exit(this);
                }
            });
            t.Start();*/
            await DBRegister("Error", component.ToString(), methodName, exception.Message);
        }

        public async Task WarningAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            /*Thread t = new Thread(() =>
            {
                Monitor.Enter(this);
                try
                {
                    DBRegister("Warning", component.ToString(), methodName, message);
                }
                finally
                {
                    Monitor.Exit(this);
                }
            });
            t.Start();*/
            await DBRegister("Warning", component.ToString(), methodName, message);
        }

        public async Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            /*Thread t = new Thread(() =>
            {
                Monitor.Enter(this);
                try
                {
                    DBRegister("Fatal", component.ToString(), methodName, exception.Message);
                }
                finally
                {
                    Monitor.Exit(this);
                }
            });
            t.Start();*/
            await DBRegister("Fatal", component.ToString(), methodName, exception.Message);
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
         
         
         
         */



    }
}
