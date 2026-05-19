using System;
using System.Collections.Generic;

namespace ropadeportiva
{
    class Program
    {
        static void Main(string[] args)
        {
            GestorProductos gestorProductos = new GestorProductos();
            GestorClientes gestorClientes = new GestorClientes();
            GestorVentas gestorVentas = new GestorVentas();

            bool salir = false;

            while (!salir)
            {
                MostrarMenuPrincipal();
                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        GestionarProductos(gestorProductos);
                        break;
                    case "2":
                        GestionarClientes(gestorClientes);
                        break;
                    case "3":
                        GestionarVentas(gestorVentas, gestorProductos, gestorClientes);
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
        static void GestionarProductos(GestorProductos gestor)
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
        static void AgregarProducto(GestorProductos gestor)
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
            gestor.AgregarProducto(nuevoProducto);
        }

        // Actualizar Producto
        static void ActualizarProducto(GestorProductos gestor)
        {
            Console.Clear();
            Console.WriteLine("========== ACTUALIZAR PRODUCTO ==========");

            Console.Write("ID del producto a actualizar: ");
            int id = int.Parse(Console.ReadLine());

            Producto productoExistente = gestor.ObtenerProducto(id);
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
            gestor.ActualizarProducto(id, productoActualizado);
        }

        // Eliminar Producto
        static void EliminarProducto(GestorProductos gestor)
        {
            Console.Clear();
            Console.WriteLine("========== ELIMINAR PRODUCTO ==========");

            Console.Write("ID del producto a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            gestor.EliminarProducto(id);
        }

        // GESTOR DE CLIENTES
        static void GestionarClientes(GestorClientes gestor)
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
        static void AgregarCliente(GestorClientes gestor)
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
            gestor.AgregarCliente(nuevoCliente);
        }

        // Actualizar Cliente
        static void ActualizarCliente(GestorClientes gestor)
        {
            Console.Clear();
            Console.WriteLine("========== ACTUALIZAR CLIENTE ==========");

            Console.Write("ID del cliente a actualizar: ");
            int id = int.Parse(Console.ReadLine());

            Cliente clienteExistente = gestor.ObtenerCliente(id);
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
            gestor.ActualizarCliente(id, clienteActualizado);
        }

        // Eliminar Cliente
        static void EliminarCliente(GestorClientes gestor)
        {
            Console.Clear();
            Console.WriteLine("========== ELIMINAR CLIENTE ==========");

            Console.Write("ID del cliente a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            gestor.EliminarCliente(id);
        }

        // GESTOR DE VENTAS
        static void GestionarVentas(GestorVentas gestor, GestorProductos gestorProductos, GestorClientes gestorClientes)
        {
            bool volver = false;

            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("========== GESTIÓN DE VENTAS ==========");
                Console.WriteLine("1. Ver todas las ventas");
                Console.WriteLine("2. Registrar nueva venta");
                Console.WriteLine("3. Ver ventas de un cliente");
                Console.WriteLine("4. Eliminar venta");
                Console.WriteLine("5. Volver al menú principal");
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
                        VerVentasPorCliente(gestor);
                        break;
                    case "4":
                        EliminarVenta(gestor);
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

        // Registrar Venta
        static void RegistrarVenta(GestorVentas gestor, GestorProductos gestorProductos, GestorClientes gestorClientes)
        {
            Console.Clear();
            Console.WriteLine("========== REGISTRAR VENTA ==========");

            Console.Write("ID de la venta: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("ID del cliente: ");
            int clienteId = int.Parse(Console.ReadLine());

            // Validar que el cliente exista
            if (gestorClientes.ObtenerCliente(clienteId) == null)
            {
                Console.WriteLine("✗ Cliente no encontrado");
                return;
            }

            Console.Write("ID del producto: ");
            int productoId = int.Parse(Console.ReadLine());

            // Validar que el producto exista
            Producto producto = gestorProductos.ObtenerProducto(productoId);
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
            gestor.AgregarVenta(nuevaVenta);

            // Actualizar stock del producto
            int nuevoStock = producto.GetCantidadStock() - cantidad;
            Producto productoActualizado = new Producto(
                producto.GetId(), 
                producto.GetNombre(), 
                producto.GetTalla(), 
                producto.GetPrecio(), 
                nuevoStock
            );
            gestorProductos.ActualizarProducto(productoId, productoActualizado);
            Console.WriteLine($"✓ Stock actualizado. Nuevo stock: {nuevoStock}");
        }

        // Ver Ventas por Cliente
        static void VerVentasPorCliente(GestorVentas gestor)
        {
            Console.Clear();
            Console.WriteLine("========== VER VENTAS POR CLIENTE ==========");

            Console.Write("ID del cliente: ");
            int clienteId = int.Parse(Console.ReadLine());

            gestor.MostrarVentasPorCliente(clienteId);
        }

        // Eliminar Venta
        static void EliminarVenta(GestorVentas gestor)
        {
            Console.Clear();
            Console.WriteLine("========== ELIMINAR VENTA ==========");

            Console.Write("ID de la venta a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            gestor.EliminarVenta(id);
        }
    }
}
