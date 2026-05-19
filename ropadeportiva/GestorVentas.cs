using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Globalization;

namespace ropadeportiva
{
    public class GestorVentas : GestorBase<Venta>, IGestorVentas
    {
        private List<Venta> ventas;
        private string rutaArchivo;

        public event EventHandler<VentaEventArgs>? VentaRegistrada;

        protected virtual void OnVentaRegistrada(Venta venta)
            => VentaRegistrada?.Invoke(this, new VentaEventArgs(venta));

        // Constructor
        public GestorVentas()
        {
            ventas = new List<Venta>();
            rutaArchivo = "Ventas.csv";
            CargarVentas();
        }

        // Cargar ventas desde el CSV
        public void CargarVentas()
        {
            try
            {
                // Si el archivo no existe, crear una lista vacía
                if (!File.Exists(rutaArchivo))
                {
                    ventas = new List<Venta>();
                    return;
                }

                // Leer el CSV con CsvHelper
                using (var reader = new StreamReader(rutaArchivo))
                using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("es-ES")))
                {
                    // Leer los registros del CSV
                    var registros = csv.GetRecords<dynamic>().ToList();

                    ventas = new List<Venta>();
                    foreach (var registro in registros)
                    {
                        var venta = new Venta(
                            int.Parse(registro.id),
                            int.Parse(registro.clienteId),
                            int.Parse(registro.productoId),
                            int.Parse(registro.cantidad),
                            DateTime.Parse(registro.fecha)
                        );
                        ventas.Add(venta);
                    }
                }

                Console.WriteLine($"✓ Se cargaron {ventas.Count} ventas desde el CSV");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al cargar ventas: {ex.Message}");
            }
        }

        // Guardar ventas en el CSV
        public void GuardarVentas()
        {
            try
            {
                using (var writer = new StreamWriter(rutaArchivo))
                using (var csv = new CsvWriter(writer, CultureInfo.GetCultureInfo("es-ES")))
                {
                    // Escribir encabezados
                    csv.WriteField("id");
                    csv.WriteField("clienteId");
                    csv.WriteField("productoId");
                    csv.WriteField("cantidad");
                    csv.WriteField("fecha");
                    csv.NextRecord();

                    // Escribir cada venta
                    foreach (var venta in ventas)
                    {
                        csv.WriteField(venta.GetId());
                        csv.WriteField(venta.GetClienteId());
                        csv.WriteField(venta.GetProductoId());
                        csv.WriteField(venta.GetCantidad());
                        csv.WriteField(venta.GetFecha().ToString("yyyy-MM-dd HH:mm:ss"));
                        csv.NextRecord();
                    }
                }

                Console.WriteLine("✓ Ventas guardadas correctamente en el CSV");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al guardar ventas: {ex.Message}");
            }
        }

        // CREATE - Agregar una nueva venta
        public void AgregarVenta(Venta venta)
        {
            try
            {
                // Verificar que el ID no exista
                if (ventas.Any(v => v.GetId() == venta.GetId()))
                {
                    Console.WriteLine("✗ Error: Ya existe una venta con ese ID");
                    return;
                }

                ventas.Add(venta);
                GuardarVentas();
                OnVentaRegistrada(venta);
                Console.WriteLine("✓ Venta registrada exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al agregar venta: {ex.Message}");
            }
        }

        public override void Agregar(Venta venta) => AgregarVenta(venta);

        // READ - Obtener una venta por ID
        public Venta ObtenerVenta(int id)
        {
            return ventas.FirstOrDefault(v => v.GetId() == id);
        }

        public override Venta Obtener(int id) => ObtenerVenta(id);

        public override void Actualizar(int id, Venta ventaActualizada)
        {
            try
            {
                var ventaExistente = ObtenerVenta(id);
                if (ventaExistente == null)
                {
                    Console.WriteLine("✗ Error: Venta no encontrada");
                    return;
                }

                int indice = ventas.IndexOf(ventaExistente);
                ventas[indice] = ventaActualizada;
                GuardarVentas();
                Console.WriteLine("✓ Venta actualizada exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al actualizar venta: {ex.Message}");
            }
        }

        public override void Eliminar(int id) => EliminarVenta(id);

        public override void MostrarTodos()
        {
            if (ventas.Count == 0)
            {
                Console.WriteLine("No hay ventas registradas");
                return;
            }

            Console.WriteLine("\n========== LISTA DE VENTAS ==========");
            foreach (var venta in ventas)
            {
                Console.WriteLine(venta);
            }
            Console.WriteLine("====================================\n");
        }

        // READ - Obtener todas las ventas
        public override List<Venta> ObtenerTodos()
        {
            return ventas;
        }

        // READ - Obtener ventas de un cliente específico
        public List<Venta> ObtenerVentasPorCliente(int clienteId)
        {
            return ventas.Where(v => v.GetClienteId() == clienteId).ToList();
        }

        // READ - Obtener ventas de un producto específico
        public List<Venta> ObtenerVentasPorProducto(int productoId)
        {
            return ventas.Where(v => v.GetProductoId() == productoId).ToList();
        }

        public List<Venta> ObtenerVentasEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return ventas
                .Where(v => v.GetFecha() >= fechaInicio && v.GetFecha() <= fechaFin)
                .ToList();
        }

        public double CalcularTotalVentas(Func<int, double> obtenerPrecioProducto)
        {
            return ventas
                .Select(v => v.GetCantidad() * obtenerPrecioProducto(v.GetProductoId()))
                .Aggregate(0.0, (total, monto) => total + monto);
        }

        // DELETE - Eliminar una venta
        public void EliminarVenta(int id)
        {
            try
            {
                var venta = ObtenerVenta(id);
                if (venta == null)
                {
                    Console.WriteLine("✗ Error: Venta no encontrada");
                    return;
                }

                ventas.Remove(venta);
                GuardarVentas();
                Console.WriteLine("✓ Venta eliminada exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al eliminar venta: {ex.Message}");
            }
        }

        // Mostrar ventas de un cliente
        public void MostrarVentasPorCliente(int clienteId)
        {
            var ventasCliente = ObtenerVentasPorCliente(clienteId);

            if (ventasCliente.Count == 0)
            {
                Console.WriteLine($"El cliente con ID {clienteId} no tiene ventas registradas");
                return;
            }

            Console.WriteLine($"\n========== VENTAS DEL CLIENTE {clienteId} ==========");
            foreach (var venta in ventasCliente)
            {
                Console.WriteLine(venta);
            }
            Console.WriteLine("==============================================\n");
        }
    }
}
