using LoanComparison.Common.DTO;
using LoanComparison.Common.Exceptions;
using LoanComparison.Service.DTO;
using LoanComparison.Service.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LoanComparison.Service.Services
{
    public class LoanComparisonService : ILoanComparisonService
    {
        private readonly IHttpService httpService;
        private readonly IOptions<LoanCalculatorRequestParam> requestConfiguration;
        private readonly IConfiguration configuration;
        public LoanComparisonService(IHttpService httpService,
            IOptions<LoanCalculatorRequestParam> requestConfiguration,
             IConfiguration configuration)
        {
            this.requestConfiguration = requestConfiguration;
            this.configuration = configuration;
            this.httpService = httpService;
        }

        #region Public Methods
        /// <summary>
        /// Gets the saving amount for a given borrowing amount and customer current rate
        /// </summary>
        /// <param name="loanDetail">LoanDetail with borrowing amount and current rate</param>
        /// <returns>Saving amount as decimal</returns>
        public async Task<decimal> GetSavingAmountAsync(LoanDetail loanDetail)
        {
            //get loan comparison request instance
            var loanComparisonRequest = GetLoanComparisonRequest(loanDetail);

            //serialize input and get the json content
            var content = JsonContent.Create(loanComparisonRequest);

            //get endpoint url from config
            var url = GetEndpointUrl("LoanComparison");

            //invoke api to get the loan comparision
            var response = await httpService.PostAsync(url, content);

            //if httpstatus is unsuccessful
            if (!response.IsSuccessStatusCode)
            {
                //throw exception with error code detial
                throw new LoanComparisonRequestException(response.StatusCode);
            }

            //deserialize response content
            var loanComparisonResponse = JsonConvert.DeserializeObject<LoanComparisonResponse>(await response.Content.ReadAsStringAsync());

            //return TotalSaved
            return loanComparisonResponse.TotalSaved;
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// Get new LoanComparisonRequest instance
        /// </summary>
        /// <param name="loanDetail">LoanDetail with borrowing amount and current rate</param>
        /// <returns>LoanComparisonRequest instance</returns>
        private LoanComparisonRequest GetLoanComparisonRequest(LoanDetail loanDetail)
        {
            return new LoanComparisonRequest()
            {
                Merchant = requestConfiguration.Value.Merchant,
                Lender = requestConfiguration.Value.Lender,
                RateType = requestConfiguration.Value.RateType,
                RepaymentType = requestConfiguration.Value.RepaymentType,
                PropertyUsage = requestConfiguration.Value.PropertyUsage,
                LoanTerm = requestConfiguration.Value.LoanTerm,
                RateTerm = requestConfiguration.Value.RateTerm,
                CustomerRate = loanDetail.CustomerRate,
                BorrowingAmount = loanDetail.BorrowingAmount
            };
        }


        /// <summary>
        /// Get endpoint url for given endpoint config name
        /// </summary>
        /// <param name="endpointConfigName">Name used in the config file</param>
        /// <returns>Endpoint url</returns>
        private string GetEndpointUrl(string endpointConfigName)
        {
            //get endpoint from config section
            return configuration.GetValue<string>($"LoanCalculatorEndpoints:{endpointConfigName}");
        }

        #endregion
    }
}
