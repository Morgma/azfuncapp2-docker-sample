using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using System.Threading.Tasks;

namespace Morgma.AzureFuncApp.DockerAnonAuthSample
{
    public static class SampleFunction
    {
        [FunctionName("Function")]
        public static async Task<HttpResponseMessage> Function(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a key authorized request.");

            RequestModel requestModel = await req.Content.ReadAsAsync<RequestModel>();

            var msg = $"Hello, {requestModel?.Name ?? "Unknown"}; you used a key!";

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(msg)
            };

            return response;

        }

        [FunctionName("Anonymous")]
        public static async Task<HttpResponseMessage> Anonymous(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed an anonymous request.");

            RequestModel requestModel = await req.Content.ReadAsAsync<RequestModel>();

            var msg = $"Hello, {requestModel?.Name ?? "Unknown"}; you didn't use a key!";

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(msg)
            };

            return response;

        }

        private class RequestModel
        {
            public string Name { get; set; }
        }

    }
}
