using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebAPIProductos.Models; // de aqui sacaremos DBContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBAPIContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

// para  controlar las referencias ciclicas de un json cuando llamas a la BBDD
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var Cors_policies = "Cors_Rules";
builder.Services.AddCors(e =>
{
    e.AddPolicy(name: Cors_policies, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Cors_policies);

app.UseAuthorization();

app.MapControllers();

app.Run();
