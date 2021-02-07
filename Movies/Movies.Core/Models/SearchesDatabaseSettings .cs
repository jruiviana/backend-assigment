using Movies.Core.Interfaces;

namespace Movies.Core.Models
{
    public class SearchesDatabaseSettings : ISearchesDatabaseSettings
    {
        public string SearchesCollectionName { get; set; }
        public string ApiKeysCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
