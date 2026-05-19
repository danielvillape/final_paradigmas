using System.Collections.Generic;

namespace ropadeportiva
{
    // Interfaz genérica para gestores de entidades del sistema.
    // Esta interfaz define los métodos comunes de CRUD que deben implementar
    // los gestores de productos, clientes y ventas.
    public interface IGestor<T> where T : class
    {
        void Agregar(T item);
        T Obtener(int id);
        List<T> ObtenerTodos();
        void Actualizar(int id, T item);
        void Eliminar(int id);
        void MostrarTodos();
    }
}
