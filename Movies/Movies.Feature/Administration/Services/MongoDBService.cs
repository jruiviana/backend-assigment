using MongoDB.Driver;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Feature.Administration.Services
{
    public class MongoDBService : IMongoDBService
    {
        private readonly IMongoCollection<Search> _searches;
        private readonly IMongoCollection<ApiKey> _apiKeys;

        public MongoDBService(ISearchesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _searches = database.GetCollection<Search>(settings.SearchesCollectionName);
            _apiKeys = database.GetCollection<ApiKey>(settings.ApiKeysCollectionName);

            // create first key if not exists
            CreateFirstKey();
        }

        public async Task<List<Search>> GetAsync() =>
          await (await _searches.FindAsync(search => true)).ToListAsync();

        public async Task<Search> GetAsync(string id) =>
            await (await _searches.FindAsync<Search>(search => search.Id == id)).FirstOrDefaultAsync();

        public async Task<Search> CreateAsync(Search search)
        {
            await _searches.InsertOneAsync(search);
            return search;
        }

        public async Task UpdateAsync(string id, Search searchIn) =>
            await _searches.ReplaceOneAsync(search => search.Id == id, searchIn);

        public async Task<long> RemoveAsync(Search searchIn) =>
           (await _searches.DeleteOneAsync(search => search.Id == searchIn.Id)).DeletedCount;

        public async Task<long> RemoveAsync(string id) =>
            (await _searches.DeleteOneAsync(search => search.Id == id)).DeletedCount;

        public async Task<List<Search>> GetAsync(DateTime startDate, DateTime endDate) =>
             await (await _searches.FindAsync(search => search.Timestamp >= startDate
                            && search.Timestamp <= endDate)).ToListAsync();

        public async Task<List<SearchReport>> GetSearchReportAsync(DateTime startDate, DateTime endDate)
        {
            return await _searches.Aggregate()
               .Match(k => k.Timestamp >= startDate && k.Timestamp <= endDate)
              .Group(k =>
              new
              {
                  year = k.Timestamp.Year,
                  month = k.Timestamp.Month,
                  day = k.Timestamp.Day,
                  hour = k.Timestamp.Hour,
                  minute = k.Timestamp.Minute - (k.Timestamp.Minute % 15)
              }, g => new { g.Key, Requests = g.Count() })
              .Project(r => new SearchReport
              {
                  Date = new DateTime(r.Key.year, r.Key.month, r.Key.day, r.Key.hour, r.Key.minute, 0),
                  TotalRequests = r.Requests
              })
              .SortBy(d => d.Date)
              .ToListAsync();
        }

        public async Task<ApiKey> GetApiKeyAsync(string key) =>
            await (await _apiKeys.FindAsync<ApiKey>(apiKey => apiKey.Key == key)).FirstOrDefaultAsync();

        private async Task CreateFirstKey()
        {
            var apiKey = new ApiKey
            {
                Owner = "Administration",
                Key = "68869c32-d971-40f0-8b12-7392a153cd94",
                Created = DateTime.Now
            };

            if (await GetApiKeyAsync(apiKey.Key) is null)
            {
                await _apiKeys.InsertOneAsync(apiKey);
            }
        }
    }
}
