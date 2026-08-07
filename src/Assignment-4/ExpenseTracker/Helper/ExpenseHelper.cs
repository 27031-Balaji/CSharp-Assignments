using ExpenseTracker.Enums;

namespace ExpenseTracker.Helper
{
    internal class ExpenseHelper
    {
        public bool IsValidDate(string? input, out DateOnly date)
        {
            return DateOnly.TryParseExact(input, "dd/MM/yyyy", out date);
        }

        public bool IsValidAmount(string? input, out decimal amount)
        {
            return decimal.TryParse(input, out amount) && amount > 0;
        }

        public bool IsValidSource(string? input, out IncomeSource source)
        {
            return System.Enum.TryParse(input, ignoreCase: true, out source) && System.Enum.IsDefined(source);
        }

        public bool IsValidCategory(string? input, out ExpenseCategory category)
        {
            return System.Enum.TryParse(input, ignoreCase: true, out category) && System.Enum.IsDefined(category);
        }

        public SearchType ReturnSearchType(string? input)
        {
            if (this.IsValidDate(input, out _))
            {
                return SearchType.Date;
            }

            if (this.IsValidAmount(input, out _))
            {
                return SearchType.Amount;
            }

            if (this.IsValidSource(input, out _))
            {
                return SearchType.Source;
            }

            if (this.IsValidCategory(input, out _))
            {
                return SearchType.Category;
            }

            return SearchType.Invalid;
        }
    }
}
