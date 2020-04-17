using SQLite;

namespace Quizz.DbSettings
{

    public interface ISQLiteInterface
    {
        SQLiteConnection GetSQLiteConnection();
    }
}
