namespace ropadeportiva
{
    public class Cliente : Entidad
    {
        private string email;
        private string telefono;

        // Constructor
        public Cliente(int id, string nombre, string email, string telefono) : base(id, nombre)
        {
            this.email = email;
            this.telefono = telefono;
        }

        public string GetEmail()
        {
            return email;
        }

        public string GetTelefono()
        {
            return telefono;
        }

        // Método ToString para mostrar información
        public override string ToString()
        {
            return $"ID: {id}, Nombre: {nombre}, Email: {email}, Teléfono: {telefono}";
        }
    }
}
