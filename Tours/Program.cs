using Microsoft.EntityFrameworkCore;
using Tours.Interfaces;
using Tours.Persistence;
using Tours.Persistence.Repositories;
using Tours.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
        .ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICouchDbService, CouchDbService>();
builder.Services.AddScoped<ITourAgencyService, TourAgencyService>();

var couchDbUri = new Uri(builder.Configuration.GetConnectionString("CouchDB")!);
builder.Services.AddHttpClient("CouchDB", client =>
{
    client.BaseAddress = new Uri($"{couchDbUri.Scheme}://{couchDbUri.Host}:{couchDbUri.Port}");
    
    if (!string.IsNullOrEmpty(couchDbUri.UserInfo))
    {
        var authBytes = System.Text.Encoding.ASCII.GetBytes(couchDbUri.UserInfo);
        client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();