using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Logging;
// using Microsoft.Azure.DurableTask.Core;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Logging;


// NOTE: Had to use Microsoft.Azure.WebJobs.Extensions.DurableTask version 1.8.3 (9/13/2019)
// in dependencies and NOT the latest beta 2.0.13 version.

namespace DurableFuncs_CS_FuncChaining
{
    public static class DurableFuncChaining
    {
        [FunctionName("Chaining")]
        public static async Task<object> Run(
    [OrchestrationTrigger] DurableOrchestrationContext context, ILogger log)
        {
            try
            {
                var x = await context.CallActivityAsync<object>("F1", null);
                var y = await context.CallActivityAsync<object>("F2", x);
                var z = await context.CallActivityAsync<object>("F3", y);
                return await context.CallActivityAsync<object>("F4", z);
            }
            catch (System.Exception ex)
            {
                log.LogInformation("Exception: " + ex.ToString());

            }
        }
    }
}