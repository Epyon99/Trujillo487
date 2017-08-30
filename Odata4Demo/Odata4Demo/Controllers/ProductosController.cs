using Odata4Demo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.OData;

namespace Odata4Demo.Controllers
{
    public class ProductosController : ODataController
    {
        ProductoContext db = new ProductoContext();


        private bool ProductoExiste(int key)
        {
            return db.Productos.Any(p => p.Id == key);
        }

        [EnableQuery]
        public IQueryable<Producto> Get()
        {
            return db.Productos;
        }

        [EnableQuery]
        public SingleResult<Producto> Get([FromODataUri] int key)
        {
            IQueryable<Producto> result = db.Productos.Where(g => g.Id == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Productos.Add(producto);
            await db.SaveChangesAsync();
            return Created(producto);
        }

        public async Task<IHttpActionResult> Patch([FromUri] int key, Delta<Producto> producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var entity = await db.Productos.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            producto.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExiste(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromUri] int key, Delta<Producto> producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var entity = await db.Productos.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            producto.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExiste(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(entity);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var producto = await db.Productos.FindAsync(key);
            if (producto == null)
            {
                return NotFound();
            }
            db.Productos.Remove(producto);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}