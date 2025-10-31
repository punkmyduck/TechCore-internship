using Newtonsoft.Json.Linq;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace ApiGateway.Aggregators
{
    public class DetailsAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var bookResponse = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var bookJson = JObject.Parse(bookResponse);

            var helloResponse = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var helloJson = JObject.Parse(helloResponse);

            var combined = new JObject
            {
                ["book"] = bookJson,
                ["hello"] = helloJson["hello"]
            };

            var stringContent = new StringContent(combined.ToString(), System.Text.Encoding.UTF8, "application/json");
            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "application/json");
        }
    }
}
