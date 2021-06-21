using System.ComponentModel.DataAnnotations;

namespace LoanComparison.Common.CustomAttributes
{
    public class RequiredGreaterThanZeroAttribute : ValidationAttribute
    {
        public bool IsDecimal { get; set; }

        public override bool IsValid(object value)
        {
            // return true if value is greater than 0
            return value != null &&
                (IsDecimal) ? decimal.TryParse(value.ToString(), out decimal deciamlValue) && deciamlValue > 0 :
                double.TryParse(value.ToString(), out double doubleValue) && doubleValue > 0;
        }
    }
}
