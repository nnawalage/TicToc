namespace LoanComparison.Common.DTO
{
    public class LoanCalculatorRequestParam
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
