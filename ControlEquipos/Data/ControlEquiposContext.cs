using ControlEquipos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlEquipos.Data
{
    public class ControlEquiposContext : DbContext
    {
        public ControlEquiposContext(DbContextOptions<ControlEquiposContext> options)
            : base(options)
        {
        }

        public DbSet<Equipo> Equipos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Equipo>()
                .HasIndex(e => e.Serial)
                .IsUnique();
        }
    }
}

