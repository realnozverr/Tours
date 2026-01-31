using Microsoft.AspNetCore.Mvc;
using Tours.Interfaces;
using Tours.Persistence.Models;

namespace Tours.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToursController(ITourAgencyService tourService, ICouchDbService couchService): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Tour>>> GetAll() => Ok(await tourService.GetAllToursAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Tour>> GetTourByIdAsync(int id)
    {
        var tour = await tourService.GetTourByIdAsync(id);
        return tour == null ? NotFound() : Ok(tour);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Tour tour)
    {
        await tourService.CreateTourAsync(tour);
        return CreatedAtAction(nameof(GetTourByIdAsync), new { id = tour.Id }, tour);
    }
    
    [HttpGet("filter")]
    public async Task<ActionResult<List<Tour>>> GetByCountry([FromQuery] string country) => Ok(await tourService.GetToursByCountryAsync(country));

    [HttpPost("archive")]
    public async Task<IActionResult> Archive()
    {
        var tours = await tourService.GetAllToursAsync();

        var archiveDoc = new
        {
            Model = "Tour",
            ExportDate = DateTime.Now,
            Items = tours
        };

        await couchService.CreateDatabaseAsync("archives");
        await couchService.CreateDocumentAsync("archives", archiveDoc);
        
        return Ok(new { Message = "Data exported to CouchDB successfully" });
    }

    [HttpGet("nosql/databases")]
    public async Task<IActionResult> GetCouchDbs() => Ok(await couchService.GetDatabasesAsync());
}