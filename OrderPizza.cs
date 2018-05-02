
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.ServiceBus;

namespace Deloitte.CqrsAzureDemo
{
    public static class OrderPizza
    {
        [FunctionName("OrderPizza")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, 
            [EventHub("deloittedemoeventhub")] ICollector<string> outputEvents,
            TraceWriter log)
        {

            log.Info("C# HTTP trigger function processed a OrderPizza request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var order = new PizzaOrder() 
            { 
                PizzaType = data?.pizzaType,
                Amount = data?.amount,
                Name = data?.name
            };

            //eventHubMessage = "hello world!";
            //
            
            outputEvents.Add("hello world");

            return (ActionResult)new OkObjectResult($"Thank you for your order, {order.Name}!");
        }

    }
}
