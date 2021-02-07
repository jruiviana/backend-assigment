namespace Movies.Core.Interfaces
{
    public interface ISearchesDatabaseSettings
    {
        string SearchesCollectionName { get; set; }
        string ApiKeysCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
