using Tours.Interfaces;

namespace Tours.Service;

public class CouchDbService : ICouchDbService
{
    public Task CreateDatabaseAsync(string dbName)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetDatabasesAsync()
    {
        throw new NotImplementedException();
    }

    public Task CreateDocumentAsync<T>(string dbName, T document)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetDocumentAsync(string dbName, string id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDocumentAsync(string dbName, string id, string rev)
    {
        throw new NotImplementedException();
    }
}