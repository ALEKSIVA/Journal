using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Journal
{
    public class SubjectRepository
    {
        SQLite.SQLiteConnection database;
        static object locker = new object();
        public SubjectRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLite.SQLiteConnection(databasePath);
            database.CreateTables<Subjects, Reg>();
        }

        public IEnumerable<Subjects> GetItems()
        {
            return (from i in database.Table<Subjects>() select i).ToList();
        }

        public Subjects GetItem(int id)
        {
            return database.Get<Subjects>(id);
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<Subjects>(id);
            }
        }

        public int SaveItem(Subjects item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
        public int SaveReg(Reg item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
        public IEnumerable<Reg> GetReg()
        {
            return (from i in database.Table<Reg>() select i).ToList();
        }
    }
}
