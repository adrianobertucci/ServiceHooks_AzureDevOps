using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServiceHook_Trello
{
    public static class IssueOpen
    {
        [FunctionName("IssueOpen")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            //Get Body Post Function
            var content = req;
            string BodyJson = content.ReadAsStringAsync().Result;
            JObject body = JsonConvert.DeserializeObject<JObject>(BodyJson);
            var resource = body.Value<JObject>("resource");
            var fields = resource.Value<JObject>("fields");
            var Title = fields.Value<String>("System.Title");
            var Desc = fields.Value<String>("System.Description");

            var ret = Trello.Tools.CreateCard(Title, Desc);



            return (ActionResult)new OkObjectResult(ret);
        }
    }
}
