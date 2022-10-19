using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class Appcontext : DbContext
    {
        public Appcontext(DbContextOptions<Appcontext> options)

         : base(options)
        {
        }
        public DbSet<Etudiant> etudiants { get; set; }
        public DbSet<Cour> cours { get; set; }
    }
}
