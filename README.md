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

1. Aseg�rate de que la base de datos contiene los n�meros de RUC y DNI que deseas consultar.
2. Ejecuta el proyecto en Visual Studio.
3. El m�todo `ProcessRucNumbersAsync` en `MainForm` procesar� los n�meros de documento, consultar� la informaci�n de RUC y DNI, actualizar� la base de datos y exportar� los datos a un archivo Excel.

## Estructura del Proyecto

- **ExcelExporter**: Clase para exportar datos a un archivo Excel utilizando `ClosedXML`.
- **ApiRucDni**: Clase que simula las llamadas a la API para obtener informaci�n de RUC y DNI.
- **DatabaseService**: Clase que simula la obtenci�n y actualizaci�n de datos en la base de datos.
- **SqliteLoader**: Clase que simula la carga de datos en un `DataGridView` y la obtenci�n de un `DataTable`.
- **MainForm**: Formulario principal que contiene el m�todo `ProcessRucNumbersAsync` para procesar los n�meros de documento y manejar la l�gica de consulta y exportaci�n.

## Ejemplo de C�digo
public async Task ProcessRucNumbersAsync() { var documentNumbers = _databaseService.GetRucNumbers(); int validationCounter = 0; foreach (var documentNumber in documentNumbers) { try { if (documentNumber.Length == 8) { var (nombre, aPaterno, aMaterno, message) = await _apiRucDni.GetDniInfoAsync(documentNumber); if (message == "Ha superado la cantidad de consultas mensuales") { MessageBox.Show(message, "L�mite de Consultas Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning); break; } _databaseService.UpdateDocumentInfo(documentNumber, "-", nombre, aPaterno, aMaterno, "-", "-", "-", "-", "-", "-", "-", "-", message, "DNI"); _loadLabel.Text = $"Consultando DNI {documentNumber} - {nombre} {aPaterno} {aMaterno}"; } else if (documentNumber.Length == 11) { var (rucInfo, direccion, estado, condicion, departamento, provincia, distrito, ubigeo, agente_rentencion, message) = await _apiRucDni.GetRucInfoAsync(documentNumber); if (message == "Ha superado la cantidad de consultas mensuales") { MessageBox.Show(message, "L�mite de Consultas Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning); break; } _databaseService.UpdateDocumentInfo(documentNumber, rucInfo, "-", "-", "-", direccion, departamento, provincia, distrito, ubigeo, estado, condicion, agente_rentencion, message, "RUC"); var rucNatura = documentNumber.Substring(0, 2); if (rucNatura == "10") { var dniNumber = documentNumber.Substring(2, 8); var (nombre, aPaterno, aMaterno, dniMessage) = await _apiRucDni.GetDniInfoAsync(dniNumber); if (dniMessage == "Ha superado la cantidad de consultas mensuales") { MessageBox.Show(dniMessage, "L�mite de Consultas Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning); break; } _databaseService.UpdateDocumentInfo(documentNumber, rucInfo, nombre, aPaterno, aMaterno, direccion, departamento, provincia, distrito, ubigeo, estado, condicion, agente_rentencion, dniMessage, "RUC PERSONA NATURAL"); _loadLabel.Text = $"Consultando DNI {documentNumber} - {nombre} {aPaterno} {aMaterno}"; } _loadLabel.Text = $"Consultando RUC {documentNumber} - {rucInfo}"; } else { _loadLabel.Text = $"Documento {documentNumber} inv�lido."; throw new ArgumentException("N�mero de documento inv�lido. Debe tener 8 o 11 d�gitos."); } if (validationCounter >= 25) { string tableNamedbs = "ExcelData"; _sqliteLoader.LoadDataTableToDataGridView(tableNamedbs, _dataGridView); validationCounter = 0; } else { validationCounter++; } } catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message, "Error de Autorizaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error); break; } catch (Exception ex) { MessageBox.Show($"Error al consultar el documento {documentNumber}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } }
string tableNamedb = "ExcelData";
_sqliteLoader.LoadDataTableToDataGridView(tableNamedb, _dataGridView);
_loadLabel.Visible = false;

## Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue o env�a un pull request para cualquier mejora o correcci�n.

## Licencia

Este proyecto est� licenciado bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para m�s detalles.
  