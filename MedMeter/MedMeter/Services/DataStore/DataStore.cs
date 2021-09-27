using MedMeter.Services.DataStore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedMeter.Services
{
    public class DataStore<T> : IDataStore<T> where T : new()
    {
        IDialogService DialogService;
        
        public DataStore(IDialogService dialogService)
        {
            DialogService = dialogService;

            Database.Instance.CreateTableAsync<T>();
        }

        public static event EventHandler<T> Added;
        public static event EventHandler<T> Updated;
        public static event EventHandler<string> Deleted;

        public async Task<int> AddItemAsync(T item)
        {
            try
            {
                var result = await Database.Instance.InsertAsync(item);
                Added?.Invoke(this, item);
                return result;
            }
            catch (Exception ex)
            {
                //ideally convert to a friendly message or silently log the exception
                await DialogService.DisplayAlert("Error in Database", ex.Message, "Okay");
                return await Task.FromResult(0);
            }
        }

        public async Task<int> UpdateItemAsync(T item)
        {
            try
            {
                var result = await Database.Instance.UpdateAsync(item);
                Updated?.Invoke(this, item);
                return result;
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlert("Error in Database", ex.Message, "Okay");
                return await Task.FromResult(0);
            }
        }

        public async Task<int> DeleteItemAsync(string id)
        {
            try
            {
                var result = await Database.Instance.DeleteAsync<T>(id);
                Deleted?.Invoke(this, id);
                return result;
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlert("Error in Database", ex.Message, "Okay");
                return await Task.FromResult(0);
            }
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                return await Database.Instance.GetAsync<T>(id);
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlert("Error in Database", ex.Message, "Okay");
                return await Task.FromResult<T>(default);
            }
        }

        public async Task<IList<T>> GetItemsAsync()
        {
            try
            {
                var mapping = await Database.Instance.GetMappingAsync<T>();
                return await Database.Instance.QueryAsync<T>($"select * from {mapping.TableName}");
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlert("Error in Database", ex.Message, "Okay");
                return await Task.FromResult<IList<T>>(default);
            }
        }
    }
}
