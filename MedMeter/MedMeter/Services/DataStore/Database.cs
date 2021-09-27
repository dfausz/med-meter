using SQLite;
using System;
using System.IO;

namespace MedMeter.Services.DataStore
{
    public class Database
    {
        private const string DatabaseFilename = "MedMeter.db3";

        private const SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;

        private static string DatabasePath
        {
            get
            {
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        private static SQLiteAsyncConnection instance;
        public static SQLiteAsyncConnection Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new SQLiteAsyncConnection(DatabasePath, Flags);
                }

                return instance;
            }
            protected set
            {
                instance = value;
            }
        }

    }
}
