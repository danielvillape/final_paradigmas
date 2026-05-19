using System;
using System.IO;
using Xunit;

namespace ropadeportiva.Tests;

public class GestoresTests
{
    private const string ClientesCsv = "Clientes.csv";
    private const string ProductosCsv = "Productos.csv";
    private const string VentasCsv = "Ventas.csv";

    private static void EliminarArchivoSiExiste(string ruta)
    {
        if (File.Exists(ruta))
        {
            File.Delete(ruta);
        }
    }

    [Fact]
    public void AgregarCliente_DeberiaGuardarYObtenerCliente()
    {
        EliminarArchivoSiExiste(ClientesCsv);

        var gestor = new GestorClientes();
        gestor.Agregar(new Cliente(1, "Ana", "ana@example.com", "123456789"));

        var cliente = gestor.Obtener(1);

        Assert.NotNull(cliente);
        Assert.Equal(1, cliente.GetId());
        Assert.Equal("Ana", cliente.GetNombre());
        Assert.Equal("ana@example.com", cliente.GetEmail());
    }

    [Fact]
    public void AgregarProducto_ActualizarStockYEliminarDeberiaFuncionar()
    {
        EliminarArchivoSiExiste(ProductosCsv);

        var gestor = new GestorProductos();
        var producto = new Producto(10, "Zapatillas", "M", 59.99, 8);
        gestor.Agregar(producto);

        var productoGuardado = gestor.Obtener(10);
        Assert.NotNull(productoGuardado);
        Assert.Equal(8, productoGuardado.GetCantidadStock());

        var productoActualizado = new Producto(10, "Zapatillas", "M", 59.99, 5);
        gestor.Actualizar(10, productoActualizado);

        var productoConStockReducido = gestor.Obtener(10);
        Assert.NotNull(productoConStockReducido);
        Assert.Equal(5, productoConStockReducido.GetCantidadStock());

        gestor.Eliminar(10);
        var productoEliminado = gestor.Obtener(10);
        Assert.Null(productoEliminado);
    }

    [Fact]
    public void CalcularTotalVentas_RetornaSumaCorrecta()
    {
        EliminarArchivoSiExiste(VentasCsv);

        var gestor = new GestorVentas();
        gestor.Agregar(new Venta(1, 1, 1, 2, DateTime.Now));
        gestor.Agregar(new Venta(2, 2, 2, 3, DateTime.Now));

        double total = gestor.CalcularTotalVentas(productoId => productoId == 1 ? 10.0 : 20.0);

        Assert.Equal(80.0, total);
    }
}
