# Proyecto de Consulta de RUC y DNI con Exportaci�n a Excel

Este proyecto permite consultar informaci�n de RUC y DNI, actualizar una base de datos con los resultados y exportar los datos a un archivo Excel utilizando la biblioteca `ClosedXML`.

## Caracter�sticas

- Consulta de informaci�n de RUC y DNI.
- Actualizaci�n de la base de datos con los resultados de las consultas.
- Exportaci�n de datos a un archivo Excel.
- Manejo de l�mites de consultas mensuales.
- Captura y manejo de excepciones para mostrar mensajes de error espec�ficos.

## Requisitos

- .NET Framework (versi�n compatible con tu proyecto).
- Biblioteca `ClosedXML` para la exportaci�n a Excel.

## Instalaci�n

1. Clona este repositorio en tu m�quina local.
2. Abre el proyecto en Visual Studio.
3. Instala la biblioteca `ClosedXML` a trav�s de NuGet:


## Uso

1. Genera un token de acceso en la p�gina de apiperu.dev.
1. Crea una archivo de excel en la cual la colunma A1 es la cabecera, desde la Columna A2 hacia abajo ingresa los numeros de RUC o DNI que requieras consultar.
2. Ejecuta el proyecto en Visual Studio.
3. El m�todo `ProcessRucNumbersAsync` en `MainForm` procesar� los n�meros de documento, consultar� la informaci�n de RUC y DNI, actualizar� la base de datos y exportar� los datos a un archivo Excel.

## Estructura del Proyecto

- **ExcelExporter**: Clase para exportar datos a un archivo Excel utilizando `ClosedXML`.
- **ApiRucDni**: Clase que simula las llamadas a la API para obtener informaci�n de RUC y DNI.
- **DatabaseService**: Clase que simula la obtenci�n y actualizaci�n de datos en la base de datos.
- **SqliteLoader**: Clase que simula la carga de datos en un `DataGridView` y la obtenci�n de un `DataTable`.
- **MainForm**: Formulario principal que contiene el m�todo `ProcessRucNumbersAsync` para procesar los n�meros de documento y manejar la l�gica de consulta y exportaci�n.

## Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue o env�a un pull request para cualquier mejora o correcci�n.

## Licencia

Este proyecto est� licenciado bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para m�s detalles.
  