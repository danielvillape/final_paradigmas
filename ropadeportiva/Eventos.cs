using System;

namespace ropadeportiva
{
    public class ClienteEventArgs : EventArgs
    {
        public Cliente Cliente { get; }

        public ClienteEventArgs(Cliente cliente)
        {
            Cliente = cliente;
        }
    }

    public class ProductoEventArgs : EventArgs
    {
        public Producto Producto { get; }
        public int CantidadAntigua { get; }
        public int CantidadNueva { get; }

        public ProductoEventArgs(Producto producto, int cantidadAntigua = 0, int cantidadNueva = 0)
        {
            Producto = producto;
            CantidadAntigua = cantidadAntigua;
            CantidadNueva = cantidadNueva;
        }
    }

    public class VentaEventArgs : EventArgs
    {
        public Venta Venta { get; }

        public VentaEventArgs(Venta venta)
        {
            Venta = venta;
        }
    }
}
