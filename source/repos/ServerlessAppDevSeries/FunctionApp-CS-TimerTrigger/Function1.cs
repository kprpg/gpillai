using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp_CS_TimerTrigger
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("*/1 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // Write your code with the necessary logic here.
           
        }
    }
}
//
//NCRONTAB examples
//Here are some examples of NCRONTAB expressions you can use for the timer trigger in Azure Functions.
//Example
//When triggered


//"0 */5 * * * *" once every five minutes 
//"0 0 * * * *" once at the top of every hour
//"0 0 */2 * * *" once every two hours 
//"0 0 9-17 * * *" once every hour from 9 AM to 5 PM
//"0 30 9 * * *" at 9:30 AM every day
//"0 30 9 * * 1-5" at 9:30 AM every weekday
//"0 30 9 * Jan Mon" at 9:30 AM every Monday in January
//
// 