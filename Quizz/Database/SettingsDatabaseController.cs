using SQLite;
using Xamarin.Forms;

using Quizz.Models;
using Quizz.Services;
using Quizz.ServiceModels;

namespace Quizz.DbSettings
{
    public class SettingsDatabaseController
    {

        SQLiteConnection database;
        UserService userService;

        public SettingsDatabaseController()
        {
            database = DependencyService.Get<ISQLiteInterface>().GetSQLiteConnection();
            userService = new UserService();

            database.CreateTable<Settings>();
        }

        public Settings GetSettings()
        {
            if (database.Table<Settings>().Count() == 0)
            {
                return new Settings();
            }
            else
            {
                return database.Table<Settings>().ToList()[database.Table<Settings>().Count() - 1];
            }


        }

        public async System.Threading.Tasks.Task<int> SaveSettingsAsync(Settings settingsParam, bool personalInformationChanged = false)
        {

            if (settingsParam.Id != 0)
            {
                database.Update(settingsParam);
                if (personalInformationChanged)
                {
                    Settings setting = this.GetSettings();
                    User user = App.UserDatabase.GetUser();
                    PersonalInformationModel personalInformationModel = new PersonalInformationModel
                    {
                        Username = setting.Username,
                        Email = setting.Email,
                        Password = setting.Password,
                        Token = user.Token
                    };
                    await userService.ChangeUserInformation(personalInformationModel);
                }
                return settingsParam.Id;
            }
            else
            {
                settingsParam.Id = 1;
                if (personalInformationChanged)
                {
                    Settings setting = this.GetSettings();
                    User user = App.UserDatabase.GetUser();
                    PersonalInformationModel personalInformationModel = new PersonalInformationModel
                    {
                        Username = setting.Username,
                        Email = setting.Email,
                        Password = setting.Password,
                        Token = user.Token
                    };
                    await userService.ChangeUserInformation(personalInformationModel);
                }
                return database.Insert(settingsParam);
            }

        }

        public int DeleteToken()
        {
            return database.DeleteAll<Settings>();

        }

    }
}
