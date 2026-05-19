using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace ropadeportiva
{
    class Program
    {
        static void Main(string[] args)
        {
            IWindsorContainer container = new WindsorContainer();
            container.Register(
                Component.For<LoggingInterceptor>(),
                Component.For<IGestor<Producto>, GestorProductos>().ImplementedBy<GestorProductos>().Interceptors<LoggingInterceptor>(),
                Component.For<IGestor<Cliente>, GestorClientes>().ImplementedBy<GestorClientes>().Interceptors<LoggingInterceptor>(),
                Component.For<IGestorVentas, GestorVentas>().ImplementedBy<GestorVentas>().Interceptors<LoggingInterceptor>()
            );

            GestorProductos gestorProductos = container.Resolve<GestorProductos>();
            GestorClientes gestorClientes = container.Resolve<GestorClientes>();
            GestorVentas gestorVentas = container.Resolve<GestorVentas>();

            IGestor<Producto> gestorProductosInterface = gestorProductos;
            IGestor<Cliente> gestorClientesInterface = gestorClientes;
            IGestorVentas gestorVentasInterface = gestorVentas;

            AsociarManejadoresDeEventos(gestorClientes, gestorProductos, gestorVentas);

            bool salir = false;

            while (!salir)
            {
                MostrarMenuPrincipal();
                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        GestionarProductos(gestorProductosInterface);
                        break;
                    case "2":
                        GestionarClientes(gestorClientesInterface);
                        break;
                    case "3":
                        GestionarVentas(gestorVentas, gestorProductosInterface, gestorClientesInterface);
                        break;
                    case "4":
                        salir = true;
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("✗ Opción no válida");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresiona Enter para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        static void AsociarManejadoresDeEventos(GestorClientes gestorClientes, GestorProductos gestorProductos, GestorVentas gestorVentas)
        {
            gestorClientes.ClienteAgregado += (_, e) =>
                Console.WriteLine($"[Evento] Cliente agregado: {e.Cliente.GetNombre()} (ID {e.Cliente.GetId()})");

            gestorClientes.ClienteActualizado += (_, e) =>
                Console.WriteLine($"[Evento] Cliente actualizado: {e.Cliente.GetNombre()} (ID {e.Cliente.GetId()})");

            gestorClientes.ClienteEliminado += (_, e) =>
                Console.WriteLine($"[Evento] Cliente eliminado: {e.Cliente.GetNombre()} (ID {e.Cliente.GetId()})");

            gestorProductos.ProductoAgregado += (_, e) =>
                Console.WriteLine($"[Evento] Producto agregado: {e.Producto.GetNombre()} (ID {e.Producto.GetId()})");

            gestorProductos.StockActualizado += (_, e) =>
                Console.WriteLine($"[Evento] Stock actualizado para {e.Producto.GetNombre()}: {e.CantidadAntigua} -> {e.CantidadNueva}");

            gestorVentas.VentaRegistrada += (_, e) =>
                Console.WriteLine($"[Evento] Venta registrada: ID {e.Venta.GetId()}, Cliente {e.Venta.GetClienteId()}, Producto {e.Venta.GetProductoId()}");
        }

        // MENÚ PRINCIPAL
        static void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("  TIENDA DE ROPA DEPORTIVA");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Gestionar Productos");
            Console.WriteLine("2. Gestionar Clientes");
            Console.WriteLine("3. Gestionar Ventas");
            Console.WriteLine("4. Salir");
            Console.WriteLine("========================================");
            Console.Write("Selecciona una opción: ");
        }

        // GESTOR DE PRODUCTOS
        static void GestionarProductos(IGestor<Producto> gestor)
        {
            bool volver = false;

            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("========== GESTIÓN DE PRODUCTOS ==========");
                Console.WriteLine("1. Ver todos los productos");
                Console.WriteLine("2. Agregar nuevo producto");
                Console.WriteLine("3. Actualizar producto");
                Console.WriteLine("4. Eliminar producto");
                Console.WriteLine("5. Volver al menú principal");
                Console.WriteLine("=========================================");
                Console.Write("Selecciona una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        gestor.MostrarTodos();
                        break;
                    case "2":
                        AgregarProducto(gestor);
                        break;
                    case "3":
                        ActualizarProducto(gestor);
                        break;
                    case "4":
                        EliminarProducto(gestor);
                        break;
                    case "5":
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("✗ Opción no válida");
                        break;
                }

                if (!volver)
                {
                    Console.WriteLine("\nPresiona Enter para continuar...");
                    Console.ReadLine();
                }
            }
        }

        // Agregar Producto
        static void AgregarProducto(IGestor<Producto> gestor)
        {
            Console.Clear();
            Console.WriteLine("========== AGREGAR PRODUCTO ==========");

            Console.Write("ID del producto: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Nombre del producto: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Talla (XS, S, M, L, XL, XXL): ");
            string talla = Console.ReadLine() ?? "";

            Console.Write("Precio: $");
            double precio = double.Parse(Console.ReadLine());

            Console.Write("Cantidad en stock: ");
            int cantidadStock = int.Parse(Console.ReadLine());

            Producto nuevoProducto = new Producto(id, nombre, talla, precio, cantidadStock);
            gestor.Agregar(nuevoProducto);
        }

        // Actualizar Producto
        static void ActualizarProducto(IGestor<Producto> gestor)
        {
            Console.Clear();
            Console.WriteLine("========== ACTUALIZAR PRODUCTO ==========");

            Console.Write("ID del producto a actualizar: ");
            int id = int.Parse(Console.ReadLine());

            Producto productoExistente = gestor.Obtener(id);
            if (productoExistente == null)
            {
                Console.WriteLine("✗ Producto no encontrado");
                return;
            }

            Console.WriteLine($"\nProducto actual: {productoExistente}");

            Console.Write("\nNuevo nombre: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Nueva talla: ");
            string talla = Console.ReadLine() ?? "";

            Console.Write("Nuevo precio: $");
            double precio = double.Parse(Console.ReadLine());

            Console.Write("Nueva cantidad en stock: ");
            int cantidadStock = int.Parse(Console.ReadLine());

            Producto productoActualizado = new Producto(id, nombre, talla, precio, cantidadStock);
            gestor.Actualizar(id, productoActualizado);
        }

        // Eliminar Producto
        static void EliminarProducto(IGestor<Producto> gestor)
        {
            Console.Clear();
            Console.WriteLine("========== ELIMINAR PRODUCTO ==========");

            Console.Write("ID del producto a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            gestor.Eliminar(id);
        }

        // GESTOR DE CLIENTES
        static void GestionarClientes(IGestor<Cliente> gestor)
        {
            bool volver = false;

            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("========== GESTIÓN DE CLIENTES ==========");
                Console.WriteLine("1. Ver todos los clientes");
                Console.WriteLine("2. Agregar nuevo cliente");
                Console.WriteLine("3. Actualizar cliente");
                Console.WriteLine("4. Eliminar cliente");
                Console.WriteLine("5. Volver al menú principal");
                Console.WriteLine("=========================================");
                Console.Write("Selecciona una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        gestor.MostrarTodos();
                        break;
                    case "2":
                        AgregarCliente(gestor);
                        break;
                    case "3":
                        ActualizarCliente(gestor);
                        break;
                    case "4":
                        EliminarCliente(gestor);
                        break;
                    case "5":
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("✗ Opción no válida");
                        break;
                }

                if (!volver)
                {
                    Console.WriteLine("\nPresiona Enter para continuar...");
                    Console.ReadLine();
                }
            }
        }

        // Agregar Cliente
        static void AgregarCliente(IGestor<Cliente> gestor)
        {
            Console.Clear();
            Console.WriteLine("========== AGREGAR CLIENTE ==========");

            Console.Write("ID del cliente: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine() ?? "";

            Cliente nuevoCliente = new Cliente(id, nombre, email, telefono);
            gestor.Agregar(nuevoCliente);
        }

        // Actualizar Cliente
        static void ActualizarCliente(IGestor<Cliente> gestor)
        {
            Console.Clear();
            Console.WriteLine("========== ACTUALIZAR CLIENTE ==========");

            Console.Write("ID del cliente a actualizar: ");
            int id = int.Parse(Console.ReadLine());

            Cliente clienteExistente = gestor.Obtener(id);
            if (clienteExistente == null)
            {
                Console.WriteLine("✗ Cliente no encontrado");
                return;
            }

            Console.WriteLine($"\nCliente actual: {clienteExistente}");

            Console.Write("\nNuevo nombre: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Nuevo email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Nuevo teléfono: ");
            string telefono = Console.ReadLine() ?? "";

            Cliente clienteActualizado = new Cliente(id, nombre, email, telefono);
            gestor.Actualizar(id, clienteActualizado);
        }

        // Eliminar Cliente
        static void EliminarCliente(IGestor<Cliente> gestor)
        {
            Console.Clear();
            Console.WriteLine("========== ELIMINAR CLIENTE ==========");

            Console.Write("ID del cliente a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            gestor.Eliminar(id);
        }

        // GESTOR DE VENTAS
        static void GestionarVentas(IGestorVentas gestor, IGestor<Producto> gestorProductos, IGestor<Cliente> gestorClientes)
        {
            bool volver = false;

            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("========== GESTIÓN DE VENTAS ==========");
                Console.WriteLine("1. Ver todas las ventas");
                Console.WriteLine("2. Registrar nueva venta");
                Console.WriteLine("3. Mostrar total facturado");
                Console.WriteLine("4. Ver ventas de un cliente");
                Console.WriteLine("5. Eliminar venta");
                Console.WriteLine("6. Volver al menú principal");
                Console.WriteLine("=======================================");
                Console.Write("Selecciona una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        gestor.MostrarTodos();
                        break;
                    case "2":
                        RegistrarVenta(gestor, gestorProductos, gestorClientes);
                        break;
                    case "3":
                        MostrarResumenVentas(gestor, gestorProductos);
                        break;
                    case "4":
                        VerVentasPorCliente(gestor);
                        break;
                    case "5":
                        EliminarVenta(gestor);
                        break;
                    case "6":
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("✗ Opción no válida");
                        break;
                }

                if (!volver)
                {
                    Console.WriteLine("\nPresiona Enter para continuar...");
                    Console.ReadLine();
                }
            }
        }

        // Registrar Venta
        static void RegistrarVenta(IGestorVentas gestor, IGestor<Producto> gestorProductos, IGestor<Cliente> gestorClientes)
        {
            Console.Clear();
            Console.WriteLine("========== REGISTRAR VENTA ==========");

            Console.Write("ID de la venta: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("ID del cliente: ");
            int clienteId = int.Parse(Console.ReadLine());

            // Validar que el cliente exista
            if (gestorClientes.Obtener(clienteId) == null)
            {
                Console.WriteLine("✗ Cliente no encontrado");
                return;
            }

            Console.Write("ID del producto: ");
            int productoId = int.Parse(Console.ReadLine());

            // Validar que el producto exista
            Producto producto = gestorProductos.Obtener(productoId);
            if (producto == null)
            {
                Console.WriteLine("✗ Producto no encontrado");
                return;
            }

            Console.Write("Cantidad a vender: ");
            int cantidad = int.Parse(Console.ReadLine());

            // Validar stock
            if (producto.GetCantidadStock() < cantidad)
            {
                Console.WriteLine($"✗ Stock insuficiente. Disponibles: {producto.GetCantidadStock()}");
                return;
            }

            Venta nuevaVenta = new Venta(id, clienteId, productoId, cantidad, DateTime.Now);
            gestor.Agregar(nuevaVenta);

            // Actualizar stock del producto
            int nuevoStock = producto.GetCantidadStock() - cantidad;
            Producto productoActualizado = new Producto(
                producto.GetId(), 
                producto.GetNombre(), 
                producto.GetTalla(), 
                producto.GetPrecio(), 
                nuevoStock
            );
            gestorProductos.Actualizar(productoId, productoActualizado);
            Console.WriteLine($"✓ Stock actualizado. Nuevo stock: {nuevoStock}");
        }

        // Ver Ventas por Cliente
        static void VerVentasPorCliente(IGestorVentas gestor)
        {
            Console.Clear();
            Console.WriteLine("========== VER VENTAS POR CLIENTE ==========");

            Console.Write("ID del cliente: ");
            int clienteId = int.Parse(Console.ReadLine());

            gestor.MostrarVentasPorCliente(clienteId);
        }

        // Mostrar resumen de ventas usando LINQ funcional
        static void MostrarResumenVentas(IGestorVentas gestor, IGestor<Producto> gestorProductos)
        {
            Console.Clear();
            Console.WriteLine("========== RESUMEN DE VENTAS ==========");

            double totalFacturado = gestor.CalcularTotalVentas(productoId =>
            {
                var producto = gestorProductos.Obtener(productoId);
                return producto != null ? producto.GetPrecio() : 0;
            });

            Console.WriteLine($"Total facturado: ${totalFacturado:F2}");
            Console.WriteLine($"Total ventas registradas: {gestor.ObtenerTodos().Count}");
            Console.WriteLine("======================================");
        }

        // Eliminar Venta
        static void EliminarVenta(IGestorVentas gestor)
        {
            Console.Clear();
            Console.WriteLine("========== ELIMINAR VENTA ==========");

            Console.Write("ID de la venta a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            gestor.Eliminar(id);
        }
    }
}
