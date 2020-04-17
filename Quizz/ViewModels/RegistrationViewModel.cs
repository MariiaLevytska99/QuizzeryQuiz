using System.ComponentModel;
using System.Threading.Tasks;

using Quizz.ServiceModels;
using Quizz.Services;

namespace Quizz.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly RegistrationService registrationService = new RegistrationService();
        private readonly LoginService loginService = new LoginService();

        #region properies
        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        #endregion

        #region main methods 

        public async Task<bool> Register()
        {
            RegistrationModel registration = new RegistrationModel();

            if (Password == ConfirmPassword)
            {
                registration.Username = username;
                registration.Email = email;
                registration.Password = password;
            }

            var result = await registrationService.Registration(registration);

            return result;
        }

        public async Task<bool> LogIn()
        {
            if (Username == null)
            {
                return false;
            }

            else
            {
                var result = await loginService.Login(Username, Password);

                return result;
            }
        }

        public async Task<bool> ResetPassword(string email, string password)
        {
            var result = await loginService.ResetPassword(email, password);

            return result;
        }

        #endregion
    }
}
