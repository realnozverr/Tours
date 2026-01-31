using Tours.Interfaces;
using Tours.Persistence.Models;
using Tours.Persistence.Repositories;

namespace Tours.Service;

public class TourAgencyService(IRepository<Tour> repository) : ITourAgencyService
{
    public async Task CreateTourAsync(Tour tour) => await repository.AddAsync(tour);

    public async Task UpdateTourAsync(Tour tour) => await repository.UpdateAsync(tour);

    public async Task DeleteTourAsync(int id) => await repository.DeleteAsync(id);

    public async Task<List<Tour>> GetAllToursAsync()  => await repository.GetAllAsync();

    public async Task<Tour?> GetTourByIdAsync(int id) => await repository.GetByIdAsync(id);
    public async Task<List<Tour>> GetToursByCountryAsync(string country) => await repository.FindAllAsync(t => t.Country == country);
}