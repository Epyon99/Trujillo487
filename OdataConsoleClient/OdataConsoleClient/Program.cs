using OdataConsoleClient.Default;
using OdataConsoleClient.Odata4Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdataConsoleClient
{
    class Program
    {
        // ODATA Client Code Generator
        static void Main(string[] args)
        {
            string serviceUri = "http://localhost:55908";
            var container = new Container(new Uri(serviceUri));
            var producto = new Producto()
            {
                Nombre = "Parlentes",
                Precio = 10,
            };
            AgregarProducto(container, producto);
            TodosLosProductos(container);
        }

        static void TodosLosProductos(Container container)
        {
            var query = container.Productos.Where(g=>g.Nombre == "Parlantes" && g.Id > 2);

            foreach (var p in query.ToList())
            {
                Console.WriteLine($"{p.Nombre} {p.Precio} - {p.Id}");
            }
        }

        static void AgregarProducto(Container container, Producto producto)
        {
            container.AddToProductos(producto);
            var serviceResponse = container.SaveChanges();
            foreach (var operationResponse in serviceResponse)
            {
                Console.WriteLine($"Response {operationResponse.StatusCode}");
            }
        }
    }
}
