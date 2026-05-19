using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Globalization;

namespace ropadeportiva
{
    public class GestorClientes : GestorBase<Cliente>
    {
        private List<Cliente> clientes;
        private string rutaArchivo;

        public event EventHandler<ClienteEventArgs>? ClienteAgregado;
        public event EventHandler<ClienteEventArgs>? ClienteActualizado;
        public event EventHandler<ClienteEventArgs>? ClienteEliminado;

        protected virtual void OnClienteAgregado(Cliente cliente)
            => ClienteAgregado?.Invoke(this, new ClienteEventArgs(cliente));

        protected virtual void OnClienteActualizado(Cliente cliente)
            => ClienteActualizado?.Invoke(this, new ClienteEventArgs(cliente));

        protected virtual void OnClienteEliminado(Cliente cliente)
            => ClienteEliminado?.Invoke(this, new ClienteEventArgs(cliente));

        // Constructor
        public GestorClientes()
        {
            clientes = new List<Cliente>();
            rutaArchivo = "Clientes.csv";
            CargarClientes();
        }

        // Cargar clientes desde el CSV
        public void CargarClientes()
        {
            try
            {
                // Si el archivo no existe, crear una lista vacía
                if (!File.Exists(rutaArchivo))
                {
                    clientes = new List<Cliente>();
                    return;
                }

                // Leer el CSV con CsvHelper
                using (var reader = new StreamReader(rutaArchivo))
                using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("es-ES")))
                {
                    // Leer los registros del CSV
                    var registros = csv.GetRecords<dynamic>().ToList();

                    clientes = new List<Cliente>();
                    foreach (var registro in registros)
                    {
                        var cliente = new Cliente(
                            int.Parse(registro.id),
                            registro.nombre,
                            registro.email,
                            registro.telefono
                        );
                        clientes.Add(cliente);
                    }
                }

                Console.WriteLine($"✓ Se cargaron {clientes.Count} clientes desde el CSV");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al cargar clientes: {ex.Message}");
            }
        }

        // Guardar clientes en el CSV
        public void GuardarClientes()
        {
            try
            {
                using (var writer = new StreamWriter(rutaArchivo))
                using (var csv = new CsvWriter(writer, CultureInfo.GetCultureInfo("es-ES")))
                {
                    // Escribir encabezados
                    csv.WriteField("id");
                    csv.WriteField("nombre");
                    csv.WriteField("email");
                    csv.WriteField("telefono");
                    csv.NextRecord();

                    // Escribir cada cliente
                    foreach (var cliente in clientes)
                    {
                        csv.WriteField(cliente.GetId());
                        csv.WriteField(cliente.GetNombre());
                        csv.WriteField(cliente.GetEmail());
                        csv.WriteField(cliente.GetTelefono());
                        csv.NextRecord();
                    }
                }

                Console.WriteLine("✓ Clientes guardados correctamente en el CSV");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al guardar clientes: {ex.Message}");
            }
        }

        // CREATE - Agregar un nuevo cliente
        public void AgregarCliente(Cliente cliente)
        {
            try
            {
                // Verificar que el ID no exista
                if (clientes.Any(c => c.GetId() == cliente.GetId()))
                {
                    Console.WriteLine("✗ Error: Ya existe un cliente con ese ID");
                    return;
                }

                clientes.Add(cliente);
                GuardarClientes();
                OnClienteAgregado(cliente);
                Console.WriteLine("✓ Cliente agregado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al agregar cliente: {ex.Message}");
            }
        }

        public override void Agregar(Cliente cliente) => AgregarCliente(cliente);

        // READ - Obtener un cliente por ID
        public Cliente ObtenerCliente(int id)
        {
            return clientes.FirstOrDefault(c => c.GetId() == id);
        }

        public override Cliente Obtener(int id) => ObtenerCliente(id);

        public override void Actualizar(int id, Cliente clienteActualizado) => ActualizarCliente(id, clienteActualizado);

        public override void Eliminar(int id) => EliminarCliente(id);

        // READ - Obtener todos los clientes
        public override List<Cliente> ObtenerTodos()
        {
            return clientes;
        }

        public List<Cliente> BuscarClientesPorNombre(string texto)
        {
            return clientes
                .Where(c => c.GetNombre().Contains(texto, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // UPDATE - Actualizar un cliente
        public void ActualizarCliente(int id, Cliente clienteActualizado)
        {
            try
            {
                var clienteExistente = ObtenerCliente(id);
                if (clienteExistente == null)
                {
                    Console.WriteLine("✗ Error: Cliente no encontrado");
                    return;
                }

                // Actualizar el cliente en la lista
                int indice = clientes.IndexOf(clienteExistente);
                clientes[indice] = clienteActualizado;

                GuardarClientes();
                OnClienteActualizado(clienteActualizado);
                Console.WriteLine("✓ Cliente actualizado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al actualizar cliente: {ex.Message}");
            }
        }

        // DELETE - Eliminar un cliente
        public void EliminarCliente(int id)
        {
            try
            {
                var cliente = ObtenerCliente(id);
                if (cliente == null)
                {
                    Console.WriteLine("✗ Error: Cliente no encontrado");
                    return;
                }

                clientes.Remove(cliente);
                GuardarClientes();
                OnClienteEliminado(cliente);
                Console.WriteLine("✓ Cliente eliminado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al eliminar cliente: {ex.Message}");
            }
        }

        // Mostrar todos los clientes
        public override void MostrarTodos()
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("No hay clientes registrados");
                return;
            }

            Console.WriteLine("\n========== LISTA DE CLIENTES ==========");
            foreach (var cliente in clientes)
            {
                Console.WriteLine(cliente);
            }
            Console.WriteLine("========================================\n");
        }
    }
}
