using Microsoft.Azure.WebJobs;
// using Microsoft.Azure.DurableTask.Core;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


// NOTE: Had to use Microsoft.Azure.WebJobs.Extensions.DurableTask version 1.8.3 (9/13/2019)
// in dependencies and NOT the latest beta 2.0.13 version.

namespace FunctionApp_CS_Orchestrator
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallActivityAsync<string>("DurableFunc_Welcome", "HP-Houston"));
            outputs.Add(await context.CallActivityAsync<string>("DurableFunc_Welcome", "HP-Austin"));
            outputs.Add(await context.CallActivityAsync<string>("DurableFunc_Welcome", "London"));
            outputs.Add(await context.CallActivityAsync<string>("DurableFunc_Welcome", "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>("DurableFunc_Welcome", "Seattle"));
            outputs.Add(await context.CallActivityAsync<string>("DurableFunc_Welcome", "London"));

            // returns ["Hello HP-Houston!", "Hello HP-Austin!", "Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }

        [FunctionName("DurableFunc_Welcome")]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"#######################################");
            log.LogInformation($"***### SAYING WELCOME TO {name} ###***.");
            log.LogInformation($"#######################################");
            return $"Welcome to {name}!";
        }

        [FunctionName("Function1_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("Function1", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}