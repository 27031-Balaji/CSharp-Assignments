using ExpenseTracker.ConstantLiteral;

namespace ExpenseTracker.Helper
{
    internal class AuthenticationHelper
    {
        public bool IsValidUserName(string userName)
        {
            return !string.IsNullOrWhiteSpace(userName) && userName.Length <= Constant.MaxLengthOfNameAndPassword;
        }

        public bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length <= Constant.MaxLengthOfNameAndPassword && !password.Contains(',');
        }
    }
}