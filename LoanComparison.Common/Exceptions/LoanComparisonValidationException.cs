using System;

namespace LoanComparison.Common.Exceptions
{
    public class LoanComparisonValidationException:Exception
    {
        public LoanComparisonValidationException(string message) : base(message)
        {
        }
    }
}
