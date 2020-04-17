using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Quizz.Views;
using Quizz.Models;
using Quizz.DbSettings;
using Quizz.CustomControls;

namespace Quizz
{
    public partial class App : Application
    {
        #region parameters

        public static bool isLoggined = false;
        public static string Token = string.Empty;
        public static User user = new User();
        public static Settings settings = new Settings();
        static SettingsDatabaseController settingsDatabase;
        static UserDatabaseController userDatabase;

        public static int CurrenyCategory { get; internal set; }

        public static SettingsDatabaseController SettingsDatabase
        {
            get
            {
                if (settingsDatabase == null)
                {
                    settingsDatabase = new SettingsDatabaseController();
                }

                return settingsDatabase;
            }
        }

        public static UserDatabaseController UserDatabase
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabaseController();
                }

                return userDatabase;
            }

        }

        #endregion

        public App()
        {
            InitializeComponent();
            if(App.UserDatabase.GetUser() != null)
            {
                if (App.SettingsDatabase.GetSettings().Notification)
                {
                    SendNotification();

                }
                MainPage = new MainPage();
            }

            else
            {
                SendNotificationLogin();
                MainPage = new Login();
            } 
        }

        #region appaer methods

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion

        #region notifications

        private void SendNotification()
        {
            string message = "З поверненням! Час перевірити свої знання!";
            DependencyService.Get<INotification>().CreateNotification("Quizzery", message);
        }

        private void SendNotificationLogin()
        {
            string message = "Приєднуйся до нашої команди! Вчись, перевіряй знання і шукай друзів!";
            DependencyService.Get<INotification>().CreateNotification("Quizzery", message);
        }

        #endregion
    }

}
