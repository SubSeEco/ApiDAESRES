using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using DAES.API.BackOffice.Modelos;
using static App.API.Controllers.RegistroOrganizacionRESController;

var builder = WebApplication.CreateBuilder(args);

// add document upload filter
builder.Services.AddScoped<PostMethodFilter>();

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SistemaIntegrado")));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API DAES RES", Version = "v1" });
});

string mensajeOrganizacionRESSchema = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "mensajeOrganizacionRES.schema.json"));
builder.Services.AddMemoryCache();
var app = builder.Build();

var cache = app.Services.GetRequiredService<IMemoryCache>();
cache.Set("MensajeOrganizacionRES", mensajeOrganizacionRESSchema);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API DAES RES V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
