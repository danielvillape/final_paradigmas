namespace ropadeportiva
{
    // Clase base abstracta que representa una entidad del sistema
    public abstract class Entidad
    {
        protected int id;
        protected string nombre;

        // Constructor protegido para que solo las clases derivadas lo usen
        protected Entidad(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        // Getters
        public int GetId()
        {
            return id;
        }

        public string GetNombre()
        {
            return nombre;
        }

        // Método abstracto que cada clase derivada debe implementar
        public abstract override string ToString();
    }
}
