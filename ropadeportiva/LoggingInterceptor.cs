using Castle.DynamicProxy;
using System;

namespace ropadeportiva
{
    // Interceptor AOP que registra la entrada y salida de los métodos.
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"[AOP] Iniciando {invocation.Method.Name} en {invocation.TargetType.Name}");

            try
            {
                invocation.Proceed();
                Console.WriteLine($"[AOP] Método {invocation.Method.Name} completado correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AOP] Error en {invocation.Method.Name}: {ex.Message}");
                throw;
            }
        }
    }
}
