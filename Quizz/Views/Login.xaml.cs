using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Plugin.GoogleClient;
using Quizz.ServiceModels;
using Quizz.Services;
using Quizz.ViewModel;
using Quizz.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Quizz.Views
{
    public partial class Login : ContentPage
    {
        #region parameters

        private ForgotPasswordPopup _forgotPasswordPopup;
        private RegistrationViewModel _registrationViewModel;
        private bool _loginIsCorrect = true;
        private bool _registrationIsCorrect = true;
        
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        #endregion

        public Login()
        {
            InitializeComponent();
            _registrationViewModel = new RegistrationViewModel();
            BindingContext = _registrationViewModel;
            confirmPassword.IsVisible = false;
            layoutEmail.IsVisible = false;
        }

        #region tapped functions

        async void mainsignin_Tapped(object sender, EventArgs e)
        {
            layoutEmail.IsVisible = false;
            buttonSign.IsVisible = true;
            buttonSignUp.IsVisible = false;
            mainSignIn.FontAttributes = FontAttributes.Bold;
            mainSignUp.FontAttributes = FontAttributes.None;
            await Task.WhenAll(mainBox.TranslateTo(0, 0, 120, Easing.SinOut),
                confirmPassword.TranslateTo(0, mainGrid.Height, 500, Easing.SinOut),
                layoutEmail.TranslateTo(0, mainGrid.Height, 500, Easing.SinOut),
                Social.TranslateTo(0, -confirmPassword.Height - 15, 500, Easing.SinOut),
                Social.FadeTo(1, 500));
            EmptyEntry();
            this.Title = "Вхід";
            buttonSignUp.IsVisible = false;
            buttonSign.IsVisible = true;

        }

        async void mainsignup_Tapped(object sender, EventArgs e)
        {
            buttonSign.IsVisible = false;
            buttonSignUp.IsVisible = true;
            confirmPassword.IsVisible = true;
            layoutEmail.IsVisible = true;
            mainSignUp.FontAttributes = FontAttributes.Bold;
            mainSignIn.FontAttributes = FontAttributes.None;
            await Task.WhenAll(mainBox.TranslateTo(mainBox.Width, 0, 120, Easing.SinOut),
                confirmPassword.TranslateTo(0, 0, 500, Easing.SinOut),
                layoutEmail.TranslateTo(0, 0, 500, Easing.SinOut),
                Social.TranslateTo(0, 0, 500, Easing.SinOut),
                Social.FadeTo(0, 100));
            EmptyEntry();
            this.Title = "Реєстрація";
            buttonSignUp.IsVisible = true;
            buttonSign.IsVisible = false;
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            _forgotPasswordPopup = new ForgotPasswordPopup();
            await PopupNavigation.Instance.PushAsync(_forgotPasswordPopup);
        }

        #endregion

        #region additional functions

        private void EmptyEntry()
        {
            txtUser.Text = txtPass.Text = txtConfirmPass.Text = txtEmail.Text = "";
            txtUser.Unfocus();
            txtPass.Unfocus();
            txtConfirmPass.Unfocus();
            userError.IsVisible = passwordError.IsVisible = confirmError.IsVisible = emailError.IsVisible = false;
        }

        #endregion

        #region entry text changed

        void txtUser_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string user = txtUser.Text;
            bool userEr = !string.IsNullOrEmpty(user) && !string.IsNullOrWhiteSpace(user);

            if (!userEr)
            {
                userError.IsVisible = true;
            }
            else
            {
                userError.IsVisible = false;

            }
        }

        void txtPass_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string password = txtPass.Text;
            bool pass = !string.IsNullOrEmpty(password) && !string.IsNullOrWhiteSpace(password);

            if (!pass)
            {
                passwordError.IsVisible = true;
            }
            else
            {
                passwordError.IsVisible = false;

            }
        }

        void txtConfirmPass_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            bool confirmPass = (bool)((Entry)sender).Text?.Equals(txtPass.Text);

            if (!confirmPass)
            {
                confirmError.IsVisible = true;
            }
            else
            {
                confirmError.IsVisible = false;

            }
        }

        void txtEmail_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string emailUser = txtEmail.Text;

            bool emailEr = !string.IsNullOrEmpty(emailUser) && !string.IsNullOrWhiteSpace(emailUser) && regex.Match(emailUser).Success;

            if (!emailEr)
            {
                emailError.IsVisible = true;
            }
            else
            {
                emailError.IsVisible = false;

            }
        }

        #endregion

        #region button click

        async void buttonSign_Clicked(System.Object sender, System.EventArgs e)
        {
            _loginIsCorrect = !string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty( txtPass.Text);
            if (_loginIsCorrect)
            {
                var result = await _registrationViewModel.LogIn();
                if (result)
                {
                    App.UserDatabase.AddUser(App.user);
                    App.SettingsDatabase.SaveSettingsAsync(App.settings);
                    App.isLoggined = true;
                    App.Current.MainPage = new MainPage();
                }

                else
                {
                    userError.IsVisible = passwordError.IsVisible = true;
                }
            }
        }

        async void buttonSignUp_Clicked(System.Object sender, System.EventArgs e)
        {
            _registrationIsCorrect = !string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPass.Text)
                && !string.IsNullOrEmpty(txtConfirmPass.Text) && !string.IsNullOrEmpty(txtEmail.Text)
                && (txtPass.Text == txtConfirmPass.Text);

            if (_registrationIsCorrect)
            {
                var result = await _registrationViewModel.Register();
                if (result)
                {

                    App.Current.MainPage = new Login();
                }

                else
                {
                    userError.IsVisible = passwordError.IsVisible = confirmError.IsVisible = emailError.IsVisible = true;
                }
            }
        }

        #endregion
    }
}
