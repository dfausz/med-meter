using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedMeter.Services
{
    public interface IDataStore<T>
    {
        Task<int> AddItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IList<T>> GetItemsAsync();
    }
}
