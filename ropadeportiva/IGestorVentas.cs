using System.Collections.Generic;

namespace ropadeportiva
{
    // Interfaz específica para gestor de ventas con métodos propios del dominio.
    public interface IGestorVentas : IGestor<Venta>
    {
        List<Venta> ObtenerVentasPorCliente(int clienteId);
        void MostrarVentasPorCliente(int clienteId);
    }
}
