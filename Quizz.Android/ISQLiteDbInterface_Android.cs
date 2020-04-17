using System.IO;
using SQLite;
using Xamarin.Forms;

using Quizz.DbSettings;
using Quizz.Droid;

[assembly: Dependency(typeof(GetSQLiteConnnection))]
namespace Quizz.Droid
{
    public class GetSQLiteConnnection : ISQLiteInterface
    {
        public GetSQLiteConnnection()
        {
        }
        public SQLiteConnection GetSQLiteConnection()
        {
            var fileName = "QuizzeryDb.db3";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, fileName);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}