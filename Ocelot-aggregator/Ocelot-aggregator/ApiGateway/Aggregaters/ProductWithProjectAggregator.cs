using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using PaymentService.Api;
using ProductService.Api;
using System.Dynamic;
using System.Net;

namespace ApiGateway.Aggregaters
{
    public class ProductWithProjectAggregator : IDefinedAggregator

    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            if (responses.Any(r => r.Items.Errors().Count > 0))
            {
                return new DownstreamResponse(null, HttpStatusCode.BadRequest, (List<Header>)null, null);
            }
            dynamic newResponse = new ExpandoObject();

            foreach (var context in responses)
            {
                if ((context.Items["DownstreamRoute"] as dynamic).Key == "Payment")
                {
                    var respContent = await context.Items.DownstreamResponse().Content.ReadAsStringAsync();

                    newResponse.PaymentInfo = JsonConvert.DeserializeObject<List<Payment>>(respContent);
                }
                if ((context.Items["DownstreamRoute"] as dynamic).Key == "Product")
                {
                    var respContent = await context.Items.DownstreamResponse().Content.ReadAsStringAsync();

                    newResponse.ProductInfo = JsonConvert.DeserializeObject<List<Product>>(respContent);
                }
            }

            var stringContent = new StringContent(JsonConvert.SerializeObject(newResponse));

            return new DownstreamResponse(stringContent,
                HttpStatusCode.OK,
                responses.SelectMany(x => x.Items.DownstreamResponse().Headers).ToList(),
                "OK");
        }
    }
}