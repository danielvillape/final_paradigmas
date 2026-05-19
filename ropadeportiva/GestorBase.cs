using System;
using System.Collections.Generic;

namespace ropadeportiva
{
    // Clase base abstracta para gestores de entidades.
    // Aquí se declara la estructura común de los métodos CRUD,
    // y se define una implementación estándar para mostrar registros.
    public abstract class GestorBase<T> : IGestor<T> where T : class
    {
        public abstract void Agregar(T item);
        public abstract T Obtener(int id);
        public abstract List<T> ObtenerTodos();
        public abstract void Actualizar(int id, T item);
        public abstract void Eliminar(int id);

        public virtual void MostrarTodos()
        {
            var registros = ObtenerTodos();
            if (registros == null || registros.Count == 0)
            {
                Console.WriteLine("No hay registros para mostrar");
                return;
            }

            foreach (var registro in registros)
            {
                Console.WriteLine(registro);
            }
        }
    }
}
