using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EstimasionSS.StorageHelper
{
    public class BlobHelper
    {
        CloudStorageAccount storageAccount;

        CloudBlobClient blobClient;

        public BlobHelper()
        {
            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            blobClient = storageAccount.CreateCloudBlobClient();
        }

        public CloudBlobContainer GetContainer(string containerName)
        {
            CloudBlobContainer container;

            container = blobClient.GetContainerReference(containerName);

            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);
            return container;
        }

        public void UploadToContainer(HttpPostedFileBase file, CloudBlobContainer container, string blobName)
        {
            var blob = container.GetBlockBlobReference(blobName);

            blob.UploadFromStream(file.InputStream);
        }

    }
}