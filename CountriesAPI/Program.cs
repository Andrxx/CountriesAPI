using CountriesAPI;
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();



app.MapGet("/all", () => CountriesServices.GetCountriesListAsync());

app.MapGet("/name", (string name) => CountriesServices.GetCountryAsync(name));

app.MapGet("/find", (string name) => CountriesServices.FindCountryAsync(name));

app.Run();
