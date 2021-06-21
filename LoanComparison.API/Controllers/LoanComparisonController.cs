using LoanComparison.Common.DTO;
using LoanComparison.Common.Exceptions;
using LoanComparison.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LoanComparison.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanComparisonController : ControllerBase
    {
        private readonly ILoanComparisonService service;

        public LoanComparisonController(ILoanComparisonService service)
        {
            this.service = service;
        }


        [HttpPost]
        [Route("GetSavingAmount")]
        public async Task<decimal> GetSavingAmount(LoanDetail loanDetail)
        {
            //check model state is valid
            if (ModelState.IsValid)
            {
                //invoke service to get saving amount
                return await service.GetSavingAmountAsync(loanDetail);
            }

            //if invalid model state throw validation exception
            throw new LoanComparisonValidationException("Invalid Model");

                
        }
    }
}
