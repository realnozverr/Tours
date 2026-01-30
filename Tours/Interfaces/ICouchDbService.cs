namespace Tours.Interfaces;

public interface ICouchDbService
{
    Task CreateDatabaseAsync(string dbName);
    Task<List<string>?> GetDatabasesAsync();
    Task CreateDocumentAsync<T>(string dbName, T document);
    Task<string> GetDocumentAsync(string dbName, string id);
    Task DeleteDocumentAsync(string dbName, string id, string rev);
}