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
        private readonly AuthenticationView view;
        private readonly AuthenticationHelper helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authService">The <see cref="AuthenticationService"/> used to perform authentication operations.</param>
        /// <param name="view">The <see cref="AuthenticationView"/> used to interact with the user.</param>
        /// <param name="helper">The <see cref="AuthenticationHelper"/> used to validate input.</param>
        /// <param name="applicationController">The <see cref="ApplicationController"/> to run after successful login.</param>
        public AuthenticationController(AuthenticationService authService, AuthenticationView view, AuthenticationHelper helper, ApplicationController applicationController)
        {
            this.authService = authService;
            this.view = view;
            this.helper = helper;
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
                AuthenticationOptionMenu option = this.view.ShowMenu();
                switch (option)
                {
                    case AuthenticationOptionMenu.Login:
                        if (this.Login())
                        {
                            this.applicationController.Run();
                        }

                        break;

                    case AuthenticationOptionMenu.Signup:
                        this.SignUp();
                        break;

                    case AuthenticationOptionMenu.Exit:
                        isRunning = false;
                        break;

                    case AuthenticationOptionMenu.Invalid:
                        this.view.ShowMessage(ConsoleMessages.InvalidOptionMessage, MessageType.Error);
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
                    this.view.ShowMessage(ConsoleMessages.LoginSuccessMessage, MessageType.Success);
                    this.view.ClearScreenWithKey();
                    return true;
                }
                catch (UnauthorizedAccessException)
                {
                    this.view.ShowMessage(ConsoleMessages.InvalidLoginMessage, MessageType.Error);
                    shouldContinue = this.view.AskRetry();
                }
            }

            this.view.ClearScreen();
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
                this.view.ShowMessage(ConsoleMessages.UsernameExistsMessage, MessageType.Error);
                return;
            }

            if (!this.GetValidPassword(out string password))
            {
                return;
            }

            this.authService.SignUp(userName, password);
            this.view.ShowMessage(ConsoleMessages.AccountCreatedMessage, MessageType.Success);
            this.view.ClearScreenWithKey();
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
                userName = this.view.ReadUserName();
                if (this.helper.IsValidUserName(userName))
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
                password = this.view.ReadUserPassword();
                if (this.helper.IsValidPassword(password))
                {
                    return true;
                }

                shouldContinue = this.CanRetry("password");
            }

            return false;
        }

        /// <summary>
        /// Shows an invalid-field message and asks the user whether to retry.
        /// </summary>
        /// <param name="field">The field name to include in the invalid message.</param>
        /// <returns>True if the user chose to retry, otherwise false.</returns>
        private bool CanRetry(string field)
        {
            this.view.ShowInvalidMessage(field);
            bool shouldRetry = this.view.AskRetry();
            if (!shouldRetry)
            {
                this.view.ClearScreen();
            }

            return shouldRetry;
        }
    }
}