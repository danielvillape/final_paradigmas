using System;
using System.Collections.Generic;

namespace ropadeportiva
{
    // Interfaz específica para gestor de ventas con métodos propios del dominio.
    public interface IGestorVentas : IGestor<Venta>
    {
        List<Venta> ObtenerVentasPorCliente(int clienteId);
        List<Venta> ObtenerVentasEntreFechas(DateTime fechaInicio, DateTime fechaFin);
        double CalcularTotalVentas(Func<int, double> obtenerPrecioProducto);
        void MostrarVentasPorCliente(int clienteId);
    }
}
