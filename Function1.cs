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

            //get grade strings for all students
            foreach(var student in studentList)
            {
                tasks.Add(await context.CallActivityAsync<
                    string
                    >("Function1_GetGradeString", student));
            }

            //get the topper from both the grade
            tasks.Add(await context.CallActivityAsync<
                   string
                   >("Function1_GetToppers", studentList));

            return tasks;
        }

        [FunctionName("Function1_GetGradeString")]
        public static string GetGradeString([ActivityTrigger] Student student, ILogger log)
        {
            //return the grade string
            log.LogInformation($"Printing Grade for {student.name}");
            return $"{student.name} - {GetGrade(student.percentage)} Division";
        }


        [FunctionName("Function1_GetToppers")]
        public static string GetToppers([ActivityTrigger] List<Student> studentList, ILogger log)
        {
            log.LogInformation($"Find toppers.......");
            double topperAPercentage = 0,topperBPercentage = 0;
            string topperAName = "", topperBName = "";
            //check for percentages of all students to find the list.
            foreach(Student student in studentList)
            {
                //find topper for Division A
                if(GetGrade(student.percentage) == "A" && student.percentage > topperAPercentage)
                {
                    topperAPercentage = student.percentage;
                    topperAName = student.name;
                }
                //find topper for Division B
                if (GetGrade(student.percentage) == "B" && student.percentage > topperBPercentage)
                {
                    topperBPercentage = student.percentage;
                    topperBName = student.name;
                }
            }

            //return the complete string containing toppers of both division.
            return $"Topper in Division A is {topperAName} with {topperAPercentage}%. Topper in Division B is {topperBName} with {topperBPercentage}%.";
        }

        //utility function to find grade from percentage (not a activity function)
        public static string GetGrade(double percentage)
        {
            if (percentage >= 80)
            {
                return "A";
            }

            else if (percentage > 35)
                return "B";
            else
                return "C";
        }

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