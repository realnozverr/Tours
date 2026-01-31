using Microsoft.EntityFrameworkCore;
using Tours.Interfaces;
using Tours.Persistence;
using Tours.Persistence.Repositories;
using Tours.Service;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();