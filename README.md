# Tecnicas_Adv_Prog_Final
Trabajo Práctico Final de la materia Técnicas Avanzadas de Programación de la Universidad de Palermo.

Este repositorio contiene dos aplicaciones principales:

## VTVApp.Api

### Descripción
Aplicación ASP.NET 7 que implementa el patrón CQRS con MediatR y FluentValidation. Utiliza Entity Framework Core 7 para acceder a una base de datos SQL Server. Incorpora NLog para el registro de logs y Application Insights para monitorear la salud de la aplicación.

### Proyectos de Pruebas
- **Pruebas Unitarias:** Proyecto que utiliza NSubstitute y FluentAssertions.
- **Pruebas de Integración:** Proyecto que utiliza SpecFlow con Gherkin para BDD, definiendo escenarios de uso de la API.

### Ejecución
1. Clonar el repositorio: `git clone https://github.com/Ortiz-felipe/Tecnicas_Adv_Prog_Final.git`
2. Navegar al directorio de `VTVApp.Api` y ejecutar `dotnet restore` para restaurar las dependencias.
3. Modificar el archivo `appsettings.json` para configurar la conexión a la base de datos y otras variables de entorno.
4. Ejecutar `dotnet run` para iniciar la aplicación.

### Ejecución de Pruebas
- Ejecutar `dotnet test` dentro de los directorios de los proyectos de pruebas para correr las pruebas unitarias y de integración.

## VTVApp.Web

### Descripción
Aplicación web desarrollada en React, utilizando Vite, React Router y Redux Toolkit para la gestión de estado.

### Ejecución
1. Navegar al directorio de `VTVApp.Web`.
2. Ejecutar `npm install` para instalar las dependencias.
3. Ejecutar `npm run dev` para iniciar la aplicación en modo de desarrollo.

### Configuración
Modificar el archivo `.env` para establecer las variables de entorno necesarias, como la URL de la API.
