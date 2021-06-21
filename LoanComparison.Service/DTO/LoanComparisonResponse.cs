using System.Collections.Generic;

namespace LoanComparison.Service.DTO
{
    class LoanComparisonResponse
    {
        public decimal MonthlyRepaymentDifference { get; set; }
        public double PayPeriodDifference { get; set; }
        public decimal TotalSaved { get; set; }
        public string RequestId { get; set; }
        public IList<string> Messages { get; set; }
        
    }
}
