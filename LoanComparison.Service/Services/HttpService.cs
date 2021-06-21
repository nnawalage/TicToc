using LoanComparison.Service.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoanComparison.Service.Services
{
    public class HttpService:IHttpService
    {
        private readonly IHttpClientFactory clientFactory;

        public HttpService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        /// <summary>
        /// Send Http POST request
        /// </summary>
        /// <param name="url">endpoint url</param>
        /// <param name="content">HttpContent</param>
        /// <returns>HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            //get client instance
            var client = clientFactory.CreateClient("client");
            //invoke api to get the loan comparision
            return await client.PostAsync(url, content);
        }

    }
}
