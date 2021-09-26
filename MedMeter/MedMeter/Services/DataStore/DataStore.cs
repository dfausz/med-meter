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

        private SQLiteAsyncConnection Database;

        public DataStore()
        {
            Database = new SQLiteAsyncConnection(DatabasePath, Flags);
            Database.CreateTableAsync<T>();
        }

        public static event EventHandler<T> Added;
        public static event EventHandler<T> Updated;
        public static event EventHandler<string> Deleted;

        public async Task<int> AddItemAsync(T item)
        {
            var result = await Database.InsertAsync(item);
            Added?.Invoke(this, item);
            return result;
        }

        public async Task<int> UpdateItemAsync(T item)
        {
            var result = await Database.UpdateAsync(item);
            Updated?.Invoke(this, item);
            return result;
        }

        public async Task<int> DeleteItemAsync(string id)
        {
            var result = await Database.DeleteAsync<T>(id);
            Deleted?.Invoke(this, id);
            return result;
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
