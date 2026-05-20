# 🏪 Gestor de Ropa Deportiva - Proyecto Final

## 📋 Descripción del Sistema

Aplicación de consola desarrollada en **.NET 8** para gestionar una tienda de ropa deportiva.

La solución permite:
- Crear, leer, actualizar y eliminar clientes.
- Administrar productos con tallas, precios y stock.
- Registrar ventas y consultar el historial de facturación.
- Persistir datos en archivos CSV.

---

## 🎯 Paradigmas implementados

### 1️⃣ Programación Orientada a Objetos

- `Entidad` representa propiedades comunes.
- `Cliente`, `Producto` y `Venta` son entidades del dominio.
- `GestorBase<T>` define el comportamiento CRUD común.
- `GestorClientes`, `GestorProductos` y `GestorVentas` heredan de `GestorBase<T>`.
- `IGestor<T>` y `IGestorVentas` permiten polimorfismo.

### 2️⃣ Aspectos (AOP)

- Se usa Castle Windsor para inyección de dependencias.
- `LoggingInterceptor` intercepta métodos de los gestores.
- Esto desacopla el logging de la lógica de negocio.

### 3️⃣ Programación Funcional

- Se usa LINQ en consultas, filtros y agregaciones.
- `Func<int, double>` se emplea para calcular el total de ventas.
- Métodos como `FiltrarProductos` y `CalcularTotalVentas` siguen un estilo funcional.

### 4️⃣ Eventos personalizados

- `GestorClientes` dispara `ClienteAgregado`, `ClienteActualizado` y `ClienteEliminado`.
- `GestorProductos` dispara `ProductoAgregado` y `StockActualizado`.
- `GestorVentas` dispara `VentaRegistrada`.
- La consola se suscribe a estos eventos para mostrar notificaciones.

---

## 🧱 Estructura del proyecto

```
ropadeportiva/
├── Cliente.cs
├── Entidad.cs
├── Eventos.cs
├── GestorBase.cs
├── GestorClientes.cs
├── GestorProductos.cs
├── GestorVentas.cs
├── IGestor.cs
├── IGestorVentas.cs
├── LoggingInterceptor.cs
├── Producto.cs
├── Program.cs
├── Venta.cs
├── ropadeportiva.csproj
└── bin/Debug/net8.0/
    ├── Clientes.csv
    ├── Productos.csv
    └── Ventas.csv

ropadeportiva.Tests/
├── ropadeportiva.Tests.csproj
└── UnitTest1.cs

UML.md
README.md
```

---

## 🚀 Cómo ejecutar la aplicación

### Requisitos
- .NET 8 SDK
- Git (opcional)

### Comandos

```bash
cd ropadeportiva
dotnet restore
dotnet build
dotnet run --project ropadeportiva/ropadeportiva.csproj
```

---

## 🧪 Cómo ejecutar las pruebas

```bash
dotnet test ropadeportiva.Tests/ropadeportiva.Tests.csproj
```

El proyecto incluye pruebas unitarias para los gestores de clientes, productos y ventas.

---

## 📌 Detalles de diseño

### POO
- Herencia en gestores y entidades.
- Polimorfismo mediante interfaces genéricas.
- Composición entre ventas, clientes y productos.

### AOP
- Castle Windsor resuelve dependencias.
- `LoggingInterceptor` aplica logging transversal.

### Funcional
- Uso de expresiones lambda y LINQ.
- Operaciones de filtrado y agregación declarativas.

### Eventos
- Eventos personalizados en los gestores.
- `Program.cs` suscribe y muestra alertas en consola.

---

## 📊 Diagrama UML

El diagrama de clases está documentado en `UML.md`.

---

## 🔒 Persistencia de datos

Los datos se guardan en CSV en `bin/Debug/net8.0/`:
- `Clientes.csv`
- `Productos.csv`
- `Ventas.csv`

Se utiliza `CsvHelper` para la lectura y escritura de CSV.

---

## ✅ Estado final

- Implementación completa de POO, AOP, programación funcional y eventos.
- Diagrama UML generado.
- Pruebas unitarias creadas y ejecutadas.

---

## 💬 Autor

Daniel Villa Peláez

---
