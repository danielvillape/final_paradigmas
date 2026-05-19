namespace ropadeportiva
{
    public class Venta
    {
        private int id;
        private int clienteId;
        private int productoId;
        private int cantidad;
        private DateTime fecha;

        // Constructor
        public Venta(int id, int clienteId, int productoId, int cantidad, DateTime fecha)
        {
            this.id = id;
            this.clienteId = clienteId;
            this.productoId = productoId;
            this.cantidad = cantidad;
            this.fecha = fecha;
        }

        // Getters
        public int GetId()
        {
            return id;
        }

        public int GetClienteId()
        {
            return clienteId;
        }

        public int GetProductoId()
        {
            return productoId;
        }

        public int GetCantidad()
        {
            return cantidad;
        }

        public DateTime GetFecha()
        {
            return fecha;
        }

        // Método ToString para mostrar información
        public override string ToString()
        {
            return $"ID Venta: {id}, ID Cliente: {clienteId}, ID Producto: {productoId}, Cantidad: {cantidad}, Fecha: {fecha:dd/MM/yyyy}";
        }
    }
}
