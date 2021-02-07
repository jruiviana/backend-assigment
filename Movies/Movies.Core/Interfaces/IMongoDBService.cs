using Movies.Core.Entities;
using Movies.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Core.Interfaces
{
    public interface IMongoDBService
    {
        Task<Search> CreateAsync(Search search);
        Task<List<Search>> GetAsync();
        Task<Search> GetAsync(string id);
        Task<List<Search>> GetAsync(DateTime startDate, DateTime endDate);
        Task<long> RemoveAsync(Search searchIn);
        Task<long> RemoveAsync(string id);
        Task UpdateAsync(string id, Search searchIn);
        Task<List<SearchReport>> GetSearchReportAsync(DateTime startDate, DateTime endDate);
        Task<ApiKey> GetApiKeyAsync(string key);
    }
}