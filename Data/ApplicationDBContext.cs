using APIProductos.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace APIProductos.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext( 
            DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        
        }
        
        public DbSet<Producto> Producto {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasData(

                new Producto
                {
                    IdProducto = 1,
                    Nombre = "Producto1",
                    Descripcion = "Descripcion Producto 1",
                    cantidad = 12
                },
                new Producto
                {
                    IdProducto = 2,
                    Nombre = "Producto2",
                    Descripcion = "Descripcion Producto 2",
                    cantidad = 24
                }

                );
        }
    }
}
