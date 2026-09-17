using ExpenseTracker.Enums;
using ExpenseTracker.Helper;
using ExpenseTracker.Service;
using ExpenseTracker.View;

namespace ExpenseTracker.Controller
{
    /// <summary>
    /// Coordinates the authentication flows (login, signup, exit).
    /// </summary>
    internal class AuthenticationController
    {
        private readonly AuthenticationService authService;
        private readonly ApplicationController applicationController;
        private readonly AuthenticationView authenticationView;
        private readonly AuthenticationHelper authenticationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authService">The <see cref="AuthenticationService"/> used to perform authentication operations.</param>
        /// <param name="authenticationView">The <see cref="AuthenticationView"/> used to interact with the user.</param>
        /// <param name="authenticationHelper">The <see cref="AuthenticationHelper"/> used to validate input.</param>
        /// <param name="applicationController">The <see cref="ApplicationController"/> to run after successful login.</param>
        public AuthenticationController(AuthenticationService authService, AuthenticationView authenticationView, AuthenticationHelper authenticationHelper, ApplicationController applicationController)
        {
            this.authService = authService;
            this.authenticationView = authenticationView;
            this.authenticationHelper = authenticationHelper;
            this.applicationController = applicationController;
        }

        /// <summary>
        /// Runs the authentication loop until the user chooses to exit.
        /// </summary>
        /// <returns>Always returns false when the loop finishes.</returns>
        public bool Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                if (!this.GetValidEnumChoice(this.authenticationView.GetEnumOption<AuthenticationMenuOption>, OptionMessages.Option, out AuthenticationMenuOption authOption))
                {
                    this.authenticationView.ShowMessage(ConsoleMessages.InvalidOptionMessage, MessageType.Error);
                    continue;
                }

                switch (authOption)
                {
                    case AuthenticationMenuOption.Login:
                        if (this.Login())
                        {
                            this.applicationController.Run();
                        }

                        break;

                    case AuthenticationMenuOption.Signup:
                        this.SignUp();
                        break;

                    case AuthenticationMenuOption.Exit:
                        isRunning = false;
                        break;
                }
            }

            return false;
        }

        /// <summary>
        /// Executes the login flow: prompts for credentials, attempts authentication and optionally retries on failure.
        /// </summary>
        /// <returns>True when login succeeds, otherwise false.</returns>
        private bool Login()
        {
            bool shouldContinue = true;
            while (shouldContinue)
            {
                if (!this.GetValidUserName(out string userName))
                {
                    return false;
                }

                if (!this.GetValidPassword(out string password))
                {
                    return false;
                }

                try
                {
                    this.authService.Login(userName, password);
                    this.authenticationView.ShowMessage(ConsoleMessages.LoginSuccessMessage, MessageType.Success);
                    this.authenticationView.ClearScreenWithKey();
                    return true;
                }
                catch (UnauthorizedAccessException)
                {
                    this.authenticationView.ShowMessage(ConsoleMessages.InvalidLoginMessage, MessageType.Error);
                    shouldContinue = this.authenticationView.ConfirmAction();
                }
            }

            this.authenticationView.ClearScreen();
            return false;
        }

        /// <summary>
        /// Executes the sign-up flow: collects credentials, validates uniqueness and creates the account.
        /// </summary>
        private void SignUp()
        {
            if (!this.GetValidUserName(out string userName))
            {
                return;
            }

            if (this.authService.UserNameExists(userName))
            {
                this.authenticationView.ShowMessage(ConsoleMessages.UsernameExistsMessage, MessageType.Error);
                return;
            }

            if (!this.GetValidPassword(out string password))
            {
                return;
            }

            this.authService.SignUp(userName, password);
            this.authenticationView.ShowMessage(ConsoleMessages.AccountCreatedMessage, MessageType.Success);
            this.authenticationView.ClearScreenWithKey();
        }

        /// <summary>
        /// Prompts the user until a valid user name is provided or the user cancels.
        /// </summary>
        /// <param name="userName">The valid username.</param>
        /// <returns>True when a valid user name was obtained, otherwise false.</returns>
        private bool GetValidUserName(out string userName)
        {
            userName = string.Empty;
            bool shouldContinue = true;
            while (shouldContinue)
            {
                userName = this.authenticationView.GetInput(PromptMessages.UserName);
                if (this.authenticationHelper.IsValidUserName(userName))
                {
                    return true;
                }

                shouldContinue = this.CanRetry("username");
            }

            return false;
        }

        /// <summary>
        /// Prompts the user until a valid password is provided or the user cancels.
        /// </summary>
        /// <param name="password">The valid password.</param>
        /// <returns>True when a valid password was obtained, otherwise false.</returns>
        private bool GetValidPassword(out string password)
        {
            password = string.Empty;
            bool shouldContinue = true;
            while (shouldContinue)
            {
                password = this.authenticationView.GetUserPassword();
                if (this.authenticationHelper.IsValidPassword(password))
                {
                    return true;
                }

                shouldContinue = this.CanRetry("password");
            }

            return false;
        }

        /// <summary>
        /// Validates user input and retrieves the corresponding value from the specified enum type.
        /// </summary>
        /// <typeparam name="T">The enum type to validate against.</typeparam>
        /// <param name="inputGetter">A function that returns the user input as a string.</param>
        /// <param name="field">The name of the field being validated.</param>
        /// <param name="value">When this method returns, contains the valid enum value if successful.</param>
        /// <returns>True if a valid enum value is retrieved, otherwise false.</returns>
        private bool GetValidEnumChoice<T>(Func<string> inputGetter, string field, out T value)
            where T : struct, Enum
        {
            value = default;
            T[] values = Enum.GetValues<T>();

            bool shouldContinue = true;
            while (shouldContinue)
            {
                string input = inputGetter();
                if (this.authenticationHelper.IsValidChoice(input, values.Length, out int choice))
                {
                    value = values[choice - 1];
                    return true;
                }

                shouldContinue = this.CanRetry(field);
            }

            return false;
        }

        /// <summary>
        /// Shows an invalid-input message for the specified field and asks the user whether to retry.
        /// </summary>
        /// <param name="field">The name of the field with invalid input.</param>
        /// <returns>True if the user chooses to retry, otherwise false.</returns>
        private bool CanRetry(string field)
        {
            this.authenticationView.ShowInvalidMessage(field);
            bool shouldRetry = this.authenticationView.ConfirmAction();
            if (!shouldRetry)
            {
                this.authenticationView.ClearScreen();
            }

            return shouldRetry;
        }
    }
}