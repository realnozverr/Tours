using Tours.Interfaces;
using Tours.Persistence.Models;

namespace Tours.Service;

public class TourAgencyService : ITourAgencyService
{
    public Task CreateTourAsync(Tour tour)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTourAsync(Tour tour)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTourAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Tour>> GetAllToursAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Tour?> GetTourByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}