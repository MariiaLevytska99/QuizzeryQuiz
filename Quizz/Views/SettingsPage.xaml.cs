using System;
using System.Diagnostics;
//using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Quizz.Models;
using Rg.Plugins.Popup.Services;
using Plugin.GoogleClient;
using Quizz.ViewModels;

namespace Quizz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        #region parameters

        Settings settings;
        private bool personalSettingsChanged { get; set; }
        private bool settingsChanged { get; set; }
        
        private ChangePassword _changePasswordPopup;

        #endregion

        public SettingsPage()
        {
            InitializeComponent();
            Title = "Налаштування";
            loadSettings();
            settingsChanged = false;
            personalSettingsChanged = false;
        }

        #region tapped methods

        private async void SignOut_Tapped(object sender, EventArgs e)
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Ви дійсно хочете вийти?", "", "Так", "Ні");

            if (answer)
            {
                Navigation.PopToRootAsync();
                App.UserDatabase.DeleteUser();
                App.SettingsDatabase.DeleteToken();
                App.Current.MainPage = new Login();


            }

        }

        async void ChangePassword_Tapped(System.Object sender, System.EventArgs e)
        {
            _changePasswordPopup = new ChangePassword();
            await PopupNavigation.Instance.PushAsync(_changePasswordPopup);
            personalSettingsChanged = true;
            settingsChanged = true;

        }

        #endregion

        #region switch methods

        private void NotificationSwitchSwitched(object sender, ToggledEventArgs e)
        {
            settings.Notification = e.Value;
            settingsChanged = true;
        }

        private void SoundSwitchSwitched(object sender, ToggledEventArgs e)
        {
            settings.Sound = e.Value;
            settingsChanged = true;
        }

        #endregion

        #region entry text changed


        void usernameTxt_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            settings.Username = e.NewTextValue;
            personalSettingsChanged = true;
            settingsChanged = true;
        }

        void emailTxt_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            settings.Email = e.NewTextValue;
            personalSettingsChanged = true;
            settingsChanged = true;
        }

        void passwordTxt_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            settings.Password = e.NewTextValue;
            personalSettingsChanged = true;
            settingsChanged = true;
        }

        #endregion


        #region appear methods

        protected override void OnAppearing()
        {
            loadSettings();
            personalSettingsChanged = false;
            base.OnAppearing();

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            if (settingsChanged)
            {
                var answer = await Application.Current.MainPage.DisplayAlert("Змінити налаштування?", "", "Так", "Ні");
                if (answer)
                {
                    SaveSettings();
                    await Navigation.PopAsync();
                }
            }

        }

        #endregion

        #region additional functions

        private void loadSettings()
        {
            settings = App.SettingsDatabase.GetSettings();
            NotificationSwitch.IsToggled = settings.Notification;
            SoundSwitch.IsToggled = settings.Sound;
            usernameTxt.Text = settings.Username;
            emailTxt.Text = settings.Email;

            NotificationSwitch.Toggled += (object sender, ToggledEventArgs e) =>
            {
                NotificationSwitchSwitched(sender, e);
            };


            SoundSwitch.Toggled += (object sender, ToggledEventArgs e) =>
            {
                SoundSwitchSwitched(sender, e);
            };

        }

        private void SaveSettings()
        {
            if (personalSettingsChanged)
            {
                App.SettingsDatabase.SaveSettingsAsync(settings, true);
            }
            else
                App.SettingsDatabase.SaveSettingsAsync(settings);
        }

        #endregion

    }
}
