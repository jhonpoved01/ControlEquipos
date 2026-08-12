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
    }
}

