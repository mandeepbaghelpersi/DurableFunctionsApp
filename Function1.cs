using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DurableFunctionsApp
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            //take the list of students
            List<Student> studentList = new StudentCollection().students;
            var tasks = new List<string>();

            foreach(var student in studentList)
            {
                tasks.Add(await context.CallActivityAsync<string>("Function1_GetGrade", student));
            }

            return tasks;
        }

        [FunctionName("Function1_GetGrade")]
        public static string GetGrade([ActivityTrigger] Student student, ILogger log)
        {
            log.LogInformation($"Getting grade for {student.name}");

            if(student.percentage > 80) return $"{student.name} - A Division";
            else if (student.percentage > 35) return $"{student.name} - B Division";
            else return $"{student.name} - C (Failed)";
        }

        //[FunctionName("Function1_GetTopper")]
        //public static string GetGrade([ActivityTrigger] Student student, ILogger log)
        //{
        //    log.LogInformation($"Getting grade.");

        //    if (student.percentage > 80) return $"{student.name} - A Division";
        //    else if (student.percentage > 35) return $"{student.name} - B Division";
        //    else return $"{student.name} - C (Failed)";
        //}


        [FunctionName("Function1_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("Function1", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}