using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System.IO;

namespace ImageUploader
{
    public static class AzureStorage
    {
        public const string StorageConnection = "DefaultEndpointsProtocol=https;AccountName=eljamesarandastorage;AccountKey=DP4eiMMRRR83iUfI90erZzSEgf3OIb/VlwTENPcBMAcEjjcUsQL7kyItspH6LBo9oj3IUxcZAkOcVsf5bnkQig==;EndpointSuffix=core.windows.net";

        static CloudBlobContainer GetContainer()
        {
            var account = CloudStorageAccount.Parse(StorageConnection);
            var client = account.CreateCloudBlobClient();
            return client.GetContainerReference("images");
        }

        public static async Task UploadFile(Stream stream)
        {
            var container = GetContainer();
            await container.CreateIfNotExistsAsync();

            var name = "James";
            var fileBlob = container.GetBlockBlobReference(name);
            await fileBlob.UploadFromStreamAsync(stream);
        }
    }
}
