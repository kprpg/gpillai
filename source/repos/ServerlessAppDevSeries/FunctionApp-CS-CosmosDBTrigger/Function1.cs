using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp_CS_CosmosDBTrigger
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([CosmosDBTrigger(
            databaseName: "documentdemo",
            collectionName: "Tasks",
            ConnectionStringSetting = "DBConnString",
            LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id [ " + input[0].Id + " ]");
                log.LogInformation("Actual document:\n ====BEGIN====\n" + input[0].ToString() + "\n====END====\n ");
            }
        }
    }
}
