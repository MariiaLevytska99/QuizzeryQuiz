using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quizz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword
    {
        private bool IsValid { get; set; }

        public ChangePassword()
        {
            InitializeComponent();
        }

        #region buttons click

        async void changePaswordBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            Models.Settings currentSettings = App.SettingsDatabase.GetSettings();
            currentSettings.Password = passwordtTxt.Text;
            await App.SettingsDatabase.SaveSettingsAsync(currentSettings, true);
            PopupNavigation.Instance.PopAllAsync();
        }

        void cancelBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAllAsync();
        }

        #endregion

        #region entry changed

        void passwordtTxt_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string password = passwordtTxt.Text;
            IsValid = !string.IsNullOrEmpty(password) && !string.IsNullOrWhiteSpace(password);
        }

        void confirmPasswordTxt_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            IsValid = (bool)((Entry)sender).Text?.Equals(passwordtTxt.Text);

            if(!IsValid)
            {
                errorLabel.IsVisible = true;
                changePaswordBtn.IsEnabled = false;
            }
            else
            {
                errorLabel.IsVisible = false;
                changePaswordBtn.IsEnabled = true;
               
            }

        }

        #endregion
    }
}