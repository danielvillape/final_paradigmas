# 🏪 Gestor de Ropa Deportiva - Proyecto Final

## 📋 Descripción del Sistema

Este es un sistema de gestión de ropa deportiva desarrollado en **.NET C#** como proyecto final de estudios. La aplicación demuestra la integración de **cuatro paradigmas de programación** en una solución completa y funcional.

El sistema permite:
- **Gestionar clientes**: Crear, leer, actualizar y eliminar clientes
- **Gestionar productos**: Administrar inventario de ropa deportiva (tallas, precios, stock)
- **Registrar ventas**: Crear pedidos, registrar compras y consultar historial

---

## 🎯 Paradigmas Implementados

### 1️⃣ **Programación Orientada a Objetos (POO)**

El dominio del negocio se modela con clases que representan entidades reales:

- **Cliente**: Representa a un cliente del negocio
- **Producto**: Artículo de ropa deportiva disponible
- **Venta**: Registro de una compra realizada
- **Gestor**: Clases que manejan las operaciones CRUD

**Relaciones de POO implementadas:**
- **Herencia**: Las clases `Gestor*` heredan de una clase base común
- **Composición**: `Venta` contiene referencias a `Cliente` y `Producto`
- **Agregación**: Los gestores mantienen colecciones de entidades
- **Asociación**: Relaciones entre clases

**Polimorfismo**: Los gestores implementan una interfaz común `IGestor` permitiendo tratarlos de forma genérica.

---

### 2️⃣ **Paradigma de Aspectos (AOP)**

Se implementa usando **Castle Windsor** como contenedor de inyección de dependencias.

**Características:**
- **Logging automático**: Interceptor que registra entrada/salida de métodos
- **Manejo centralizado de errores**: Captura y gestión de excepciones
- **Servicios desacoplados**: Uso de interfaces para invertir control

```csharp
// Los servicios se resuelven a través de Castle Windsor
IWindsorContainer container = new WindsorContainer();
var gestor = container.Resolve<IGestorProductos>();
```

---

### 3️⃣ **Programación Funcional**

Se aplica un enfoque funcional sobre las consultas y operaciones de datos.

**Características:**
- **LINQ**: Consultas con `Where`, `Select` y `Aggregate`
- **Funciones puras**: Sin efectos secundarios en operaciones de datos
- **Func<> / Action<>**: Parámetros de alto orden
- **Records (inmutables)**: Tipos de datos inmutables para valores

Ejemplo:
```csharp
// Consulta LINQ funcional
var totalVentas = ventas
    .Where(v => v.GetFecha() > fechaInicio)
    .Select(v => v.GetCantidad() * precioUnitario)
    .Aggregate(0.0, (acc, monto) => acc + monto);
```

---

### 4️⃣ **Programación Orientada a Eventos**

El sistema reacciona ante cambios significativos en el dominio mediante eventos personalizados.

**Eventos implementados:**
- `ClienteAgregado`: Se dispara cuando se añade un nuevo cliente
- `ProductoAgregado`: Se dispara cuando se agrega un producto
- `VentaRegistrada`: Se dispara cuando se completa una venta
- `StockActualizado`: Se dispara cuando cambia el inventario

Ejemplo:
```csharp
// Suscripción a eventos
gestor.ClienteAgregado += (sender, args) => 
{
    Console.WriteLine($"Nuevo cliente: {args.NombreCliente}");
};
```

---

## 🏗️ Estructura del Proyecto

```
ropadeportiva/
├── Cliente.cs              # Entidad Cliente
├── Producto.cs             # Entidad Producto
├── Venta.cs                # Entidad Venta
├── GestorClientes.cs       # Operaciones sobre Clientes
├── GestorProductos.cs      # Operaciones sobre Productos
├── GestorVentas.cs         # Operaciones sobre Ventas
├── Program.cs              # Punto de entrada y menú
├── bin/Debug/net8.0/       # Archivos CSV generados
│   ├── Clientes.csv
│   ├── Productos.csv
│   └── Ventas.csv
└── README.md               # Este archivo
```

---

## 🚀 Cómo Ejecutar el Proyecto

### Requisitos
- .NET 8.0 o superior
- Visual Studio Code o Visual Studio
- Git instalado

### Pasos

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/tuusuario/ropadeportiva.git
   cd ropadeportiva
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Compilar el proyecto**
   ```bash
   dotnet build
   ```

4. **Ejecutar la aplicación**
   ```bash
   dotnet run
   ```

---

## 📊 Diagrama de Clases UML

Ver el archivo `diagrama-clases.drawio` para el diagrama de clases completo que muestra las relaciones entre entidades.

---

## 📝 Decisiones de Diseño

### POO
- Se usó **herencia** para crear gestores base común
- **Composición** para manejar relaciones Cliente-Venta-Producto
- **Polimorfismo** a través de interfaces `IGestor*`

### Aspectos (AOP)
- Castle Windsor centraliza la inyección de dependencias
- Interceptores capturan llamadas a métodos para logging y errores
- Servicios registrados como interfaces en el contenedor

### Funcional
- LINQ se usa en consultas de datos (filtrado, mapeo, agregación)
- Los records almacenan datos inmutables
- `Func<>` y `Action<>` permiten pasar comportamiento como parámetro

### Eventos
- Custom `EventArgs` para transportar información relevante del dominio
- Los gestores disparan eventos en operaciones significativas (agregar, actualizar)
- La capa de presentación se suscribe a eventos para reaccionar

---

## 🔒 Persistencia de Datos

Los datos se persisten en archivos **CSV** dentro de `bin/Debug/net8.0/`:
- `Clientes.csv`: Almacena información de clientes
- `Productos.csv`: Almacena catálogo de productos
- `Ventas.csv`: Almacena historial de ventas

La serialización utiliza la librería **CsvHelper** para formato de punto y coma (`;`), compatible con Excel en sistemas hispanohablantes.

---

## 👨‍💻 Autor

**[Tu Nombre]**
Estudiante de [Tu Carrera]
Proyecto Final - [Año]

---

## 📄 Licencia

Este proyecto es de uso educativo. Todos los derechos reservados.

---

## 📞 Contacto y Dudas

Para preguntas sobre el código o el diseño, consulta la documentación en los comentarios del código fuente.

**¡Éxito con la sustentación! 🎓**
>>>>>>> 77bbff7 (Inicializar repositorio con proyecto existente)
