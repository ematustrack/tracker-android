﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace test2
{
	public interface FolioDataService<T>
	{
		Task<bool> AddItemAsync(T item);
		Task<bool> UpdateItemAsync(T item);
		Task<bool> DeleteItemAsync(T item);
        Task<IEnumerable<T>> GetItemsAsync(string st);
	}
}