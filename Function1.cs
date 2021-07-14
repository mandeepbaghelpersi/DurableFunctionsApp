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
        //-----Orchestrator Function-----//
        [FunctionName("Function1")]
        public static async Task<Student> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            //get the input object
            object param = context.GetInput<object>();
            var tasks = await context.CallActivityAsync<Student>("Function1_GetStudent", param);
            return tasks;
        }

        //-----Activity Function-----//
        [FunctionName("Function1_GetStudent")]
        public static Student GetStudent([ActivityTrigger] RequestClass obj, ILogger log)
        {
            //return the grade string
            log.LogInformation($"Looking for student with name {obj.searchParam}");

            //take the list of students
            List<Student> studentList = new StudentCollection().students;

            foreach (var student in studentList)
            {
                if (obj.searchParam.ToLower() == student.name.ToLower())
                {
                    return student;
                }
            }

            return null;
        }

        //-----Activity Function-----//
        [FunctionName("Function1_GetGradeString")]
        public static Student GetGradeString([ActivityTrigger] Student student, ILogger log)
        {
            //return the grade string
            log.LogInformation($"Printing Grade for {student.name}");
            return student;
        }

        //-----Activity Function-----//
        [FunctionName("Function1_GetToppers")]
        public static string GetToppers([ActivityTrigger] List<Student> studentList, ILogger log)
        {
            log.LogInformation($"Find toppers.......");
            double topperAPercentage = 0, topperBPercentage = 0;
            string topperAName = "", topperBName = "";
            //check for percentages of all students to find the list.
            foreach (Student student in studentList)
            {
                //find topper for Division A
                if (student.grade == "A" && student.percentage > topperAPercentage)
                {
                    topperAPercentage = student.percentage;
                    topperAName = student.name;
                }
                //find topper for Division B
                if (student.grade == "B" && student.percentage > topperBPercentage)
                {
                    topperBPercentage = student.percentage;
                    topperBName = student.name;
                }
            }

            //return the complete string containing toppers of both division.
            return $"Topper in Division A is {topperAName} with {topperAPercentage}%. Topper in Division B is {topperBName} with {topperBPercentage}%.";
        }



        [FunctionName("Function1_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, methods: "post", Route = "Search")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {

            // Function input comes from the request content.
            RequestClass eventData = await req.Content.ReadAsAsync<RequestClass>();
            log.LogInformation($"request name:'{eventData.searchParam}'.");

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("Function1", eventData);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
       

    
