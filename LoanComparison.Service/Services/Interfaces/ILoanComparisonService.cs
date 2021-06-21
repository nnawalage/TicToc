using LoanComparison.Common.DTO;
using System.Threading.Tasks;

namespace LoanComparison.Service.Services.Interfaces
{
    public interface ILoanComparisonService
    {
        /// <summary>
        /// Gets the saving amount for a given borrowing amount and customer current rate
        /// </summary>
        /// <param name="loanDetail">LoanDetail with borrowing amount and current rate</param>
        /// <returns>Saving amount as decimal</returns>
        public Task<decimal> GetSavingAmountAsync(LoanDetail loanDetail);
    }
}
