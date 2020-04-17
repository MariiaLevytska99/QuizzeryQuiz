using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Quizz.ViewModel;
using System.Text.RegularExpressions;

namespace Quizz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPopup
    {
        #region parameters

        private bool IsValid { get; set; }
        RegistrationViewModel registrationViewModel;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        #endregion

        public ForgotPasswordPopup()
        {
            InitializeComponent();
            registrationViewModel = new RegistrationViewModel();
        }

        #region button click

        async void resetPaswordBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            IsValid = passwordtTxt.Text.Length > 0 && email_Txt.Text.Length > 0
                && (passwordtTxt.Text == confirmPasswordTxt.Text);

            if (IsValid)
            {
                
                var result = await registrationViewModel.ResetPassword(email_Txt.Text, passwordtTxt.Text);

                if(result)
                {
                    await Application.Current.MainPage.DisplayAlert("Пароль був успішно змінений", "", "ОК");

                    PopupNavigation.Instance.PopAllAsync();
                }
                else

                {
                    await Application.Current.MainPage.DisplayAlert("Щось пішло не так", "", "ОК");
                    PopupNavigation.Instance.PopAllAsync();
                }
            }
            
        }

        void cancelBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAllAsync();
        }

        #endregion

        #region entry text changed

        void passwordtTxt_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string password = passwordtTxt.Text;
            bool passwordEr = !string.IsNullOrEmpty(password) && !string.IsNullOrWhiteSpace(password);

            if (!passwordEr)
            {
                passwordError.IsVisible = true;
            }
            else
            {
                passwordError.IsVisible = false;

            }
        }

        void confirmPasswordTxt_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            bool confirmPass = (bool)((Entry)sender).Text?.Equals(passwordtTxt.Text);

            if (!confirmPass)
            {
                confirmError.IsVisible = true;
            }
            else
            {
                confirmError.IsVisible = false;

            }

        }

        void email_Txt_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string email = email_Txt.Text;
            bool EmailEr = !string.IsNullOrEmpty(email) && !string.IsNullOrWhiteSpace(email) && regex.Match(email).Success;

            if (!EmailEr)
            {
                emailError.IsVisible = true;
            }
            else
            {
                emailError.IsVisible = false;

            }
        }

        #endregion
    }
}
