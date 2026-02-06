using Tours.Interfaces;

namespace Tours.Service;

public class CouchDbService(IHttpClientFactory httpClientFactory) : ICouchDbService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CouchDB");
    public async Task CreateDatabaseAsync(string dbName)
    {
        var response = await _httpClient.PutAsync($"/{dbName}", null);
        if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.PreconditionFailed)
            response.EnsureSuccessStatusCode();
    }

    public async Task<List<string>?> GetDatabasesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<string>>("/_all_dbs");
    }

    public async Task CreateDocumentAsync<T>(string dbName, T document)
    {
        var response = await _httpClient.PostAsJsonAsync($"/{dbName}", document);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateDocumentAsync<T>(string dbName, string id, T document)
    {
        var response = await _httpClient.PutAsJsonAsync($"/{dbName}/{id}", document);
        response.EnsureSuccessStatusCode();
    }

    public async Task<string> GetDocumentAsync(string dbName, string id)
    {
        return await _httpClient.GetStringAsync($"/{dbName}/{id}");
    }

    public async Task DeleteDocumentAsync(string dbName, string id, string rev)
    {
        var response = await _httpClient.DeleteAsync($"/{dbName}/{id}?rev={rev}");
    }
}