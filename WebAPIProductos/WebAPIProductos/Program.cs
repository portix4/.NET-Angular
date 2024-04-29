using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebAPIProductos.Models; // de aqui sacaremos DBContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#pragma warning disable CS8604 // Posible argumento de referencia nulo
builder.Services.AddDbContext<DBAPIContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));
#pragma warning restore CS8604 // Posible argumento de referencia nulo

// para  controlar las referencias ciclicas de un json cuando llamas a la BBDD
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
