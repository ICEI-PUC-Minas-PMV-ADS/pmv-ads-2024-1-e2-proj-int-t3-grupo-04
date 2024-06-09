using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using NextMidiaWeb.Domain.Entities;
using NextMidiaWeb.Domain.Persistence;
using NextMidiaWeb.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add contexts to the container.
#region Database Contexts
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CategoriaDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ComentarioDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<MidiaDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<MidiaTagDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<UsuarioDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<TagDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<MidiaFavoritadaDbContext>(options => options.UseSqlServer(connectionString));
#endregion

// Add services to the container.
#region API Services
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<MidiaService>();
builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<MidiaFavoritadaService>();
#endregion

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
