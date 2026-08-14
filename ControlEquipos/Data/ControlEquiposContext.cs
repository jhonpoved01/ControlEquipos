using ControlEquipos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlEquipos.Data
{
    // El DbContext representa la sesión de Entity Framework Core con la base de datos
    // y coordina las consultas y los cambios realizados sobre los equipos.
    public class ControlEquiposContext : DbContext
    {
        // Recibe las opciones registradas en Program.cs, incluida la conexión a SQL Server.
        public ControlEquiposContext(DbContextOptions<ControlEquiposContext> options)
            : base(options)
        {
        }

        // Expone la colección de equipos para consultarla y ejecutar operaciones CRUD.
        public DbSet<Equipo> Equipos { get; set; }

        // Configura reglas del modelo que deben aplicarse directamente en la base de datos.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // El índice único protege también en SQL Server la unicidad del Serial,
            // incluso si otra operación intenta omitir la validación previa del controlador.
            // HasIndex crea el índice e IsUnique impide que dos filas compartan el mismo valor.
            modelBuilder.Entity<Equipo>()
                .HasIndex(e => e.Serial)
                .IsUnique();
        }
    }
}

