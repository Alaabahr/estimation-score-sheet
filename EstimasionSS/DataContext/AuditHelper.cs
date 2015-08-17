using EstimasionSS.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EstimasionSS.DataContext
{
    public class AuditHelper
    {
        public static void AddAudit(AuditModel audit)
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("audit");

            

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(audit);

            // Execute the insert operation.
            table.Execute(insertOperation);
        }

        /// <summary>
        /// Demonstrate the most efficient storage query - the point query - where both partition key and row key are specified. 
        /// </summary>
        /// <param name="table">Sample table name</param>
        /// <param name="partitionKey">Partition key - ie - last name</param>
        /// <param name="rowKey">Row key - ie - first name</param>
        private static async Task<AuditModel> RetrieveEntityUsingPointQueryAsync(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<AuditModel>(partitionKey, rowKey);
            TableResult result = await table.ExecuteAsync(retrieveOperation);
            AuditModel audit = result.Result as AuditModel;

            return audit;
        }

        /// <summary>
        /// Demonstrate a partition scan whereby we are searching for all the entities within a partition. Note this is not as efficient 
        /// as a range scan - but definitely more efficient than a full table scan. The async API's require the user to implement 
        /// paging themselves using continuation tokens. 
        /// </summary>
        /// <param name="partitionKey">The partition within which to search</param>
        public static async Task<List<AuditModel>> PartitionScanAsync(string partitionKey)
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("audit");

            TableQuery<AuditModel> partitionScanQuery = new TableQuery<AuditModel>().Where
                (TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
            var toReturn = new List<AuditModel>();
            TableContinuationToken token = null;
            // Page through the results
            do
            {
                TableQuerySegment<AuditModel> segment = await table.ExecuteQuerySegmentedAsync(partitionScanQuery, token);
                token = segment.ContinuationToken;
                foreach (AuditModel entity in segment)
                {
                    toReturn.Add(entity);
                }
            }
            while (token != null);

            return toReturn.OrderByDescending(a=> a.Timestamp).ToList();
        }

        public static async void DeleteEntityAsync(string partitionKey, string rowKey)
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);

            TableOperation retrieveOperation = TableOperation.Retrieve<AuditModel>(partitionKey, rowKey);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("audit");

            TableResult result = await table.ExecuteAsync(retrieveOperation);
            AuditModel audit = result.Result as AuditModel;

            await DeleteEntityAsync(table, audit);

        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="table">Sample table name</param>
        /// <param name="deleteEntity">Entity to delete</param>
        private static async Task DeleteEntityAsync(CloudTable table, AuditModel deleteEntity)
        {
            if (deleteEntity == null)
            {
                throw new ArgumentNullException("deleteEntity");
            }

            TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
            await table.ExecuteAsync(deleteOperation);
        }
    }
}