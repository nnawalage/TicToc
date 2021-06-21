using LoanComparison.Common.DTO;

namespace LoanComparison.Service.DTO
{
    class LoanComparisonRequest : LoanDetail
    {
        public string Merchant { get; set; }
        public string Lender { get; set; }
        public string RateType { get; set; }
        public string RepaymentType { get; set; }
        public string PropertyUsage { get; set; }
        public int LoanTerm { get; set; }
        public string RateTerm { get; set; }
    }
}
