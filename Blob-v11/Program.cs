using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Blob_v11
{
    class Program
    {
        static string _conenction_string = "BlobEndpoint=https://demouser326.blob.core.windows.net/;QueueEndpoint=https://demouser326.queue.core.windows.net/;FileEndpoint=https://demouser326.file.core.windows.net/;TableEndpoint=https://demouser326.table.core.windows.net/;SharedAccessSignature=sv=2020-02-10&ss=bfqt&srt=sco&sp=rwdlacupx&se=2021-04-06T05:49:41Z&st=2021-04-05T21:49:41Z&spr=https&sig=Yq%2FjzZQpk28IC%2FTJATNJhZUBzHhxPu1pGErPDuuX1Aw%3D";
        static string _container_name = "demo3";
        static string _filename = "sample.txt";
        static string _filelocation = "C:\\sample.txt";
        static void Main(string[] args)
        {
            CreateContainer().Wait();
           // UploadBlob().Wait();
           //ListBlobs().Wait();
           // GetBlob().Wait();
            Console.WriteLine("Completed");
        }

        static async Task CreateContainer()
        {
            CloudStorageAccount _storageAccount;
            if (CloudStorageAccount.TryParse(_conenction_string, out _storageAccount))
            {
                CloudBlobClient _client = _storageAccount.CreateCloudBlobClient();
                CloudBlobContainer _container = _client.GetContainerReference(_container_name);
                await _container.CreateAsync();

            }
        }

        static async Task UploadBlob()
        {
            CloudStorageAccount _storageAccount;
            if (CloudStorageAccount.TryParse(_conenction_string, out _storageAccount))
            {
                CloudBlobClient _client = _storageAccount.CreateCloudBlobClient();
                CloudBlobContainer _container = _client.GetContainerReference(_container_name);

                CloudBlockBlob _blob = _container.GetBlockBlobReference(_filename);
                await _blob.UploadFromFileAsync(_filelocation);
            }
        }
        static async Task ListBlobs()
        {
            CloudStorageAccount _storageAccount;
            if (CloudStorageAccount.TryParse(_conenction_string, out _storageAccount))
            {
                CloudBlobClient _client = _storageAccount.CreateCloudBlobClient();
                CloudBlobContainer _container = _client.GetContainerReference(_container_name);

                BlobContinuationToken _blobContinuationToken = null;
                do
                {
                    var _results = await _container.ListBlobsSegmentedAsync(null, _blobContinuationToken);

                    _blobContinuationToken = _results.ContinuationToken;

                    foreach (IListBlobItem _blob in _results.Results)
                    {
                        Console.WriteLine(_blob.Uri);
                    }
                }
                while (_blobContinuationToken != null);
            }
        }

            static async Task GetBlob()
            {
                CloudStorageAccount _storageAccount;
                if (CloudStorageAccount.TryParse(_conenction_string, out _storageAccount))
                {
                    CloudBlobClient _client = _storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer _container = _client.GetContainerReference(_container_name);
                    
                    CloudBlockBlob _blob = _container.GetBlockBlobReference(_filename);
                    string str=await _blob.DownloadTextAsync();
                    Console.WriteLine(str);
                }
                }
            }
        }

