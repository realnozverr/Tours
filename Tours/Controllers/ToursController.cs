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

    [HttpGet("{id}", Name = "GetTourById")]
    public async Task<ActionResult<Tour>> GetTourByIdAsync(int id)
    {
        var tour = await tourService.GetTourByIdAsync(id);
        return tour == null ? NotFound() : Ok(tour);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Tour tour)
    {
        await tourService.CreateTourAsync(tour);
        return CreatedAtAction("GetTourById", new { id = tour.Id }, tour);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Tour tour)
    {
        await tourService.UpdateTourAsync(tour);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tour = await tourService.GetTourByIdAsync(id);
        if (tour == null)
        {
            return NotFound();
        }
        
        await tourService.DeleteTourAsync(id);
        return NoContent();
    }
    
    [HttpGet("filter")]
    public async Task<ActionResult<List<Tour>>> GetByCountry([FromQuery] string country) => Ok(await tourService.GetToursByCountryAsync(country));

    [HttpPost("archive")]
    public async Task<IActionResult> Archive()
    {
        // 1. ИЗВЛЕЧЕНИЕ: Получение структурированных данных из реляционной БД через ORM-слой
        var tours = await tourService.GetAllToursAsync();

        // 2. ПРЕОБРАЗОВАНИЕ: Создание составного JSON-документа для NoSQL хранилища
        var archiveDoc = new
        {
            Model = "Tour",
            ExportDate = DateTime.Now,
            Items = tours
        };
        
        // 3. СОХРАНЕНИЕ: Отправка документа в CouchDB через подготовленный сервис
        await couchService.CreateDatabaseAsync("archives");
        await couchService.CreateDocumentAsync("archives", archiveDoc);
        
        // 4. РЕЗУЛЬТАТ: Уведомление клиента об успешном выполнении
        return Ok(new { Message = "Data exported to CouchDB successfully" });
    }

    [HttpGet("nosql/databases")]
    public async Task<IActionResult> GetCouchDbs() => Ok(await couchService.GetDatabasesAsync());
}