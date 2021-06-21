using LoanComparison.Common.CustomAttributes;

namespace LoanComparison.Common.DTO
{
    public class LoanDetail 
    {
        [RequiredGreaterThanZero]
        public double CustomerRate { get; set; }

        [RequiredGreaterThanZero(IsDecimal =true)]
        public decimal BorrowingAmount { get; set; }
    }
}
