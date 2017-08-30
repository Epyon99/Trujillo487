using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Odata4Demo.Models
{
    public class ProductoContext : DbContext
    {
        public ProductoContext():base("name=ProductosContext")
        {

        }

        public DbSet<Producto> Productos { get; set; }
    }
}