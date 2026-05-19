using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Globalization;

namespace ropadeportiva
{
    public class GestorProductos : GestorBase<Producto>
    {
        private List<Producto> productos;
        private string rutaArchivo;

        // Constructor
        public GestorProductos()
        {
            productos = new List<Producto>();
            rutaArchivo = "Productos.csv";
            CargarProductos();
        }

        // Cargar productos desde el CSV
        public void CargarProductos()
        {
            try
            {
                // Si el archivo no existe, crear una lista vacía
                if (!File.Exists(rutaArchivo))
                {
                    productos = new List<Producto>();
                    return;
                }

                // Leer el CSV con CsvHelper
                using (var reader = new StreamReader(rutaArchivo))
                using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("es-ES")))
                {
                    // Leer los registros del CSV
                    var registros = csv.GetRecords<dynamic>().ToList();

                    productos = new List<Producto>();
                    foreach (var registro in registros)
                    {
                        var producto = new Producto(
                            int.Parse(registro.id),
                            registro.nombre,
                            registro.talla,
                            double.Parse(registro.precio),
                            int.Parse(registro.cantidadStock)
                        );
                        productos.Add(producto);
                    }
                }

                Console.WriteLine($"✓ Se cargaron {productos.Count} productos desde el CSV");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al cargar productos: {ex.Message}");
            }
        }

        // Guardar productos en el CSV
        public void GuardarProductos()
        {
            try
            {
                using (var writer = new StreamWriter(rutaArchivo))
                using (var csv = new CsvWriter(writer, CultureInfo.GetCultureInfo("es-ES")))
                {
                    // Escribir encabezados
                    csv.WriteField("id");
                    csv.WriteField("nombre");
                    csv.WriteField("talla");
                    csv.WriteField("precio");
                    csv.WriteField("cantidadStock");
                    csv.NextRecord();

                    // Escribir cada producto
                    foreach (var producto in productos)
                    {
                        csv.WriteField(producto.GetId());
                        csv.WriteField(producto.GetNombre());
                        csv.WriteField(producto.GetTalla());
                        csv.WriteField(producto.GetPrecio());
                        csv.WriteField(producto.GetCantidadStock());
                        csv.NextRecord();
                    }
                }

                Console.WriteLine("✓ Productos guardados correctamente en el CSV");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al guardar productos: {ex.Message}");
            }
        }

        // CREATE - Agregar un nuevo producto
        public void AgregarProducto(Producto producto)
        {
            try
            {
                // Verificar que el ID no exista
                if (productos.Any(p => p.GetId() == producto.GetId()))
                {
                    Console.WriteLine("✗ Error: Ya existe un producto con ese ID");
                    return;
                }

                productos.Add(producto);
                GuardarProductos();
                Console.WriteLine("✓ Producto agregado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al agregar producto: {ex.Message}");
            }
        }

        public override void Agregar(Producto producto) => AgregarProducto(producto);

        // READ - Obtener un producto por ID
        public Producto ObtenerProducto(int id)
        {
            return productos.FirstOrDefault(p => p.GetId() == id);
        }

        public override Producto Obtener(int id) => ObtenerProducto(id);

        public override void Actualizar(int id, Producto productoActualizado) => ActualizarProducto(id, productoActualizado);

        public override void Eliminar(int id) => EliminarProducto(id);

        // READ - Obtener todos los productos
        public override List<Producto> ObtenerTodos()
        {
            return productos;
        }

        // UPDATE - Actualizar un producto
        public void ActualizarProducto(int id, Producto productoActualizado)
        {
            try
            {
                var productoExistente = ObtenerProducto(id);
                if (productoExistente == null)
                {
                    Console.WriteLine("✗ Error: Producto no encontrado");
                    return;
                }

                // Actualizar el producto en la lista
                int indice = productos.IndexOf(productoExistente);
                productos[indice] = productoActualizado;

                GuardarProductos();
                Console.WriteLine("✓ Producto actualizado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al actualizar producto: {ex.Message}");
            }
        }

        // DELETE - Eliminar un producto
        public void EliminarProducto(int id)
        {
            try
            {
                var producto = ObtenerProducto(id);
                if (producto == null)
                {
                    Console.WriteLine("✗ Error: Producto no encontrado");
                    return;
                }

                productos.Remove(producto);
                GuardarProductos();
                Console.WriteLine("✓ Producto eliminado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al eliminar producto: {ex.Message}");
            }
        }

        // Mostrar todos los productos
        public override void MostrarTodos()
        {
            if (productos.Count == 0)
            {
                Console.WriteLine("No hay productos registrados");
                return;
            }

            Console.WriteLine("\n========== LISTA DE PRODUCTOS ==========");
            foreach (var producto in productos)
            {
                Console.WriteLine(producto);
            }
            Console.WriteLine("========================================\n");
        }
    }
}
