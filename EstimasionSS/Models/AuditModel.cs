using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace EstimasionSS.Models
{
    public class AuditModel : TableEntity
    {
        public AuditModel()
        {

        }

        public AuditModel(string partitionKey)
        {

            this.PartitionKey = partitionKey;

            this.RowKey = Guid.NewGuid().ToString();
        }
        public string ActionTaken { get; set; }
    }
}