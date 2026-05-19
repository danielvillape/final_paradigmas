namespace ropadeportiva
{
    public class Producto : Entidad
    {
        private string talla;
        private double precio;
        private int cantidadStock;

        // Constructor
        public Producto(int id, string nombre, string talla, double precio, int cantidadStock) : base(id, nombre)
        {
            this.talla = talla;
            this.precio = precio;
            this.cantidadStock = cantidadStock;
        }

        public string GetTalla()
        {
            return talla;
        }

        public double GetPrecio()
        {
            return precio;
        }

        public int GetCantidadStock()
        {
            return cantidadStock;
        }

        // Setter para cantidadStock (usado cuando hay ventas)
        public void SetCantidadStock(int nuevaCantidad)
        {
            this.cantidadStock = nuevaCantidad;
        }

        // Método ToString para mostrar información
        public override string ToString()
        {
            return $"ID: {id}, Nombre: {nombre}, Talla: {talla}, Precio: ${precio}, Stock: {cantidadStock}";
        }
    }
}
