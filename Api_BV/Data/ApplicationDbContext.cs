using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Api_BV.Models;
using Microsoft.AspNetCore.Identity;

namespace Api_BV.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Subcategoria> Subcategoria { get; set; }
        public DbSet<Libros> Libros { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

    }
}
