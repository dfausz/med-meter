using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MedMeter.Services
{
    public class DataStore<T> : IDataStore<T> where T : new()
    {
        private const string DatabaseFilename = "TodoSQLite.db3";

        private const SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;

        private string DatabasePath
        {
            get
            {
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        private static SQLiteAsyncConnection Database;

        public DataStore()
        {
            Database = new SQLiteAsyncConnection(DatabasePath, Flags);
            Database.CreateTableAsync<T>();
        }

        public async Task<int> AddItemAsync(T item)
        {
            return await Database.InsertAsync(item);
        }

        public async Task<int> UpdateItemAsync(T item)
        {
            return await Database.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync(string id)
        {
            return await Database.DeleteAsync<T>(id);
        }

        public async Task<T> GetItemAsync(string id)
        {
            return await Database.GetAsync<T>(id);
        }

        public async Task<IList<T>> GetItemsAsync()
        {
            var mapping = await Database.GetMappingAsync<T>();
            return await Database.QueryAsync<T>($"select * from {mapping.TableName}");
        }
    }
}
