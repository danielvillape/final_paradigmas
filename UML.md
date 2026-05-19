# Diagrama UML - Tienda de Ropa Deportiva

## Descripción general
Este diagrama muestra las clases principales del proyecto, sus relaciones de herencia, las interfaces utilizadas y las dependencias principales.

## Diagrama de clases (Mermaid)

```mermaid
classDiagram
    class Entidad {
        - int id
        - string nombre
        + int GetId()
        + string GetNombre()
        + abstract string ToString()
    }

    class Cliente {
        - string email
        - string telefono
        + string GetEmail()
        + string GetTelefono()
        + override string ToString()
    }

    class Producto {
        - string talla
        - double precio
        - int cantidadStock
        + string GetTalla()
        + double GetPrecio()
        + int GetCantidadStock()
        + void SetCantidadStock(int nuevaCantidad)
        + override string ToString()
    }

    class Venta {
        - int id
        - int clienteId
        - int productoId
        - int cantidad
        - DateTime fecha
        + int GetId()
        + int GetClienteId()
        + int GetProductoId()
        + int GetCantidad()
        + DateTime GetFecha()
        + override string ToString()
    }

    class IGestor~T~ {
        + void Agregar(T item)
        + T Obtener(int id)
        + List~T~ ObtenerTodos()
        + void Actualizar(int id, T item)
        + void Eliminar(int id)
        + void MostrarTodos()
    }

    class GestorBase~T~ {
        + abstract void Agregar(T item)
        + abstract T Obtener(int id)
        + abstract List~T~ ObtenerTodos()
        + abstract void Actualizar(int id, T item)
        + abstract void Eliminar(int id)
        + virtual void MostrarTodos()
    }

    class IGestorVentas {
        + List~Venta~ ObtenerVentasPorCliente(int clienteId)
        + List~Venta~ ObtenerVentasEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        + double CalcularTotalVentas(Func~int, double~ obtenerPrecioProducto)
        + void MostrarVentasPorCliente(int clienteId)
    }

    class GestorClientes {
        - List~Cliente~ clientes
        - string rutaArchivo
        + event EventHandler~ClienteEventArgs~ ClienteAgregado
        + event EventHandler~ClienteEventArgs~ ClienteActualizado
        + event EventHandler~ClienteEventArgs~ ClienteEliminado
        + void CargarClientes()
        + void GuardarClientes()
        + void AgregarCliente(Cliente cliente)
        + Cliente ObtenerCliente(int id)
        + List~Cliente~ BuscarClientesPorNombre(string texto)
        + void ActualizarCliente(int id, Cliente clienteActualizado)
        + void EliminarCliente(int id)
    }

    class GestorProductos {
        - List~Producto~ productos
        - string rutaArchivo
        + event EventHandler~ProductoEventArgs~ ProductoAgregado
        + event EventHandler~ProductoEventArgs~ StockActualizado
        + void CargarProductos()
        + void GuardarProductos()
        + void AgregarProducto(Producto producto)
        + Producto ObtenerProducto(int id)
        + List~Producto~ FiltrarProductos(Func~Producto, bool~ criterio)
        + List~Producto~ ObtenerProductosEnStock()
        + void ActualizarProducto(int id, Producto productoActualizado)
        + void EliminarProducto(int id)
    }

    class GestorVentas {
        - List~Venta~ ventas
        - string rutaArchivo
        + event EventHandler~VentaEventArgs~ VentaRegistrada
        + void CargarVentas()
        + void GuardarVentas()
        + void AgregarVenta(Venta venta)
        + Venta ObtenerVenta(int id)
        + List~Venta~ ObtenerVentasPorCliente(int clienteId)
        + List~Venta~ ObtenerVentasPorProducto(int productoId)
        + List~Venta~ ObtenerVentasEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        + double CalcularTotalVentas(Func~int, double~ obtenerPrecioProducto)
        + void EliminarVenta(int id)
        + void MostrarVentasPorCliente(int clienteId)
    }

    class Program {
        + static void Main(string[] args)
        + static void AsociarManejadoresDeEventos(GestorClientes gestorClientes, GestorProductos gestorProductos, GestorVentas gestorVentas)
    }

    Entidad <|-- Cliente
    Entidad <|-- Producto
    IGestor~Cliente~ <|.. GestorClientes
    IGestor~Producto~ <|.. GestorProductos
    IGestor~Venta~ <|.. GestorVentas
    GestorBase~T~ <|-- GestorClientes
    GestorBase~T~ <|-- GestorProductos
    GestorBase~T~ <|-- GestorVentas
    IGestorVentas <|.. GestorVentas
    Program ..> IGestor~Producto~
    Program ..> IGestor~Cliente~
    Program ..> IGestorVentas
    Program ..> GestorClientes
    Program ..> GestorProductos
    Program ..> GestorVentas
``` 

## Relaciones clave
- `Cliente` y `Producto` heredan de `Entidad`.
- `GestorBase<T>` implementa `IGestor<T>` y proporciona la base común de CRUD.
- `GestorClientes`, `GestorProductos` y `GestorVentas` extienden `GestorBase<T>`.
- `GestorVentas` implementa además `IGestorVentas` para lógica de ventas específica.
- `Program` usa DI con Castle Windsor para resolver gestores y conecta el flujo de la aplicación.
- Los gestores de datos persisten en CSV (`Clientes.csv`, `Productos.csv`, `Ventas.csv`).

## Nota
El diagrama expresa tanto arquitectura de clases como las dependencias principales del programa.
