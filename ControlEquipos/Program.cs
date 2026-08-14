using ControlEquipos.Data;
using Microsoft.EntityFrameworkCore;



// Crea el constructor de la aplicación y carga su configuración,
// incluidas las cadenas de conexión definidas para el entorno actual.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Registra los servicios necesarios para usar controladores y vistas en el flujo MVC.
builder.Services.AddControllersWithViews();

// Registra el contexto de Entity Framework Core para que pueda inyectarse en los controladores.
// UseSqlServer indica que la persistencia usa SQL Server con la cadena de conexión configurada.
builder.Services.AddDbContext<ControlEquiposContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ControlEquiposConnection")));

// Construye la aplicación con los servicios registrados anteriormente.
var app = builder.Build();

// Configure the HTTP request pipeline.
// En producción, centraliza el manejo de errores y aplica HSTS para reforzar el uso de HTTPS.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Este middleware prepara cada solicitud para usar HTTPS, enrutamiento y autorización.
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Define la ruta MVC predeterminada: inicia en Home/Index y permite un Id opcional.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


// Inicia la aplicación y queda a la espera de solicitudes HTTP.
app.Run();
