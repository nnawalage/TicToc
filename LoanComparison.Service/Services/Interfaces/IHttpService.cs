using System.Net.Http;
using System.Threading.Tasks;

namespace LoanComparison.Service.Services.Interfaces
{
    public interface IHttpService
    {
        /// <summary>
        /// Send Http POST request
        /// </summary>
        /// <param name="url">endpoint url</param>
        /// <param name="content">HttpContent</param>
        /// <returns>HttpResponseMessage</returns>
        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
    }
}
