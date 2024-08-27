# Proyecto de Consulta de RUC y DNI con Exportación a Excel

Este proyecto permite consultar información de RUC y DNI, actualizar una base de datos con los resultados y exportar los datos a un archivo Excel utilizando la biblioteca `ClosedXML`.

## Características

- Consulta de información de RUC y DNI.
- Actualización de la base de datos con los resultados de las consultas.
- Exportación de datos a un archivo Excel.
- Manejo de límites de consultas mensuales.
- Captura y manejo de excepciones para mostrar mensajes de error específicos.

## Requisitos

- .NET Framework (versión compatible con tu proyecto).
- Biblioteca `ClosedXML` para la exportación a Excel.

## Instalación

1. Clona este repositorio en tu máquina local.
2. Abre el proyecto en Visual Studio.
3. Instala la biblioteca `ClosedXML` a través de NuGet:


## Uso

1. Genera un token de acceso en la página de apiperu.dev.
1. Crea una archivo de excel en la cual la colunma A1 es la cabecera, desde la Columna A2 hacia abajo ingresa los numeros de RUC o DNI que requieras consultar.
2. Ejecuta el proyecto en Visual Studio.
3. El método `ProcessRucNumbersAsync` en `MainForm` procesará los números de documento, consultará la información de RUC y DNI, actualizará la base de datos y exportará los datos a un archivo Excel.

## Estructura del Proyecto

- **ExcelExporter**: Clase para exportar datos a un archivo Excel utilizando `ClosedXML`.
- **ApiRucDni**: Clase que simula las llamadas a la API para obtener información de RUC y DNI.
- **DatabaseService**: Clase que simula la obtención y actualización de datos en la base de datos.
- **SqliteLoader**: Clase que simula la carga de datos en un `DataGridView` y la obtención de un `DataTable`.
- **MainForm**: Formulario principal que contiene el método `ProcessRucNumbersAsync` para procesar los números de documento y manejar la lógica de consulta y exportación.

## Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue o envía un pull request para cualquier mejora o corrección.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más detalles.
  