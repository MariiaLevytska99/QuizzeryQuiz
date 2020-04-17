using SQLite;
using Xamarin.Forms;

using Quizz.Models;

namespace Quizz.DbSettings
{
    public class UserDatabaseController
    {

        SQLiteConnection database;

        public UserDatabaseController()
        {
            database = DependencyService.Get<ISQLiteInterface>().GetSQLiteConnection();
            database.CreateTable<User>();
        }

        public User GetUser()
        {
            if (database.Table<User>().Count() == 0)
            {
                return null;
            }
            else
            {
                return database.Table<User>().ToList()[database.Table<User>().Count() - 1];
            }


        }

        public int AddUser(User userParam)
        {
            if (userParam.Id != 0)
            {
                database.Update(userParam);

                return userParam.Id;
            }
            else
            {
                userParam.Id = 1;

                return database.Insert(userParam);
            }
        }


        public int DeleteUser()
        {
            return database.DeleteAll<User>();


        }

    }
}
