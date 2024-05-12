using NextMidiaApi.Domain.Entities;
using NextMidiaApi.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add contexts to the container.
#region Database Contexts
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CategoriaDbContext>(options => options.UseSqlServer(connectionString,
    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "categoria")));

builder.Services.AddDbContext<ComentarioDbContext>(options => options.UseSqlServer(connectionString,
    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "comentario")));

builder.Services.AddDbContext<MidiaDbContext>(options => options.UseSqlServer(connectionString,
    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "midia")));

builder.Services.AddDbContext<MidiaTagDbContext>(options => options.UseSqlServer(connectionString,
    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "midiatag")));

builder.Services.AddDbContext<UsuarioDbContext>(options => options.UseSqlServer(connectionString,
    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "usuario")));

builder.Services.AddDbContext<TagDbContext>(options => options.UseSqlServer(connectionString,
    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "tag")));
#endregion

// Add services to the container.
#region API Services
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<MidiaService>();
builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<UsuarioService>();
#endregion

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();