using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace test2
{
	public interface ISTDataService<T>
	{
		Task<bool> AddItemAsync(T item);
		Task<bool> UpdateItemAsync(T item);
		Task<bool> DeleteItemAsync(T item);
		Task<T> GetItemAsync(string id);
		Task<IEnumerable<T>> GetSTItemsAsync(bool forceRefresh = false);
    }
}
