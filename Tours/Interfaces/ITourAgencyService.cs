using Tours.Persistence.Models;

namespace Tours.Interfaces;

public interface ITourAgencyService
{
    Task CreateTourAsync(Tour tour);
    Task UpdateTourAsync(Tour tour);
    Task DeleteTourAsync(int id);
    Task<List<Tour>> GetAllToursAsync();
    Task<Tour?> GetTourByIdAsync(int id);
    Task<List<Tour>> GetToursByCountryAsync(string country);
}