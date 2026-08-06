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
    }
}
