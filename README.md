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

1. Asegúrate de que la base de datos contiene los números de RUC y DNI que deseas consultar.
2. Ejecuta el proyecto en Visual Studio.
3. El método `ProcessRucNumbersAsync` en `MainForm` procesará los números de documento, consultará la información de RUC y DNI, actualizará la base de datos y exportará los datos a un archivo Excel.

## Estructura del Proyecto

- **ExcelExporter**: Clase para exportar datos a un archivo Excel utilizando `ClosedXML`.
- **ApiRucDni**: Clase que simula las llamadas a la API para obtener información de RUC y DNI.
- **DatabaseService**: Clase que simula la obtención y actualización de datos en la base de datos.
- **SqliteLoader**: Clase que simula la carga de datos en un `DataGridView` y la obtención de un `DataTable`.
- **MainForm**: Formulario principal que contiene el método `ProcessRucNumbersAsync` para procesar los números de documento y manejar la lógica de consulta y exportación.

## Ejemplo de Código
public async Task ProcessRucNumbersAsync() { var documentNumbers = _databaseService.GetRucNumbers(); int validationCounter = 0; foreach (var documentNumber in documentNumbers) { try { if (documentNumber.Length == 8) { var (nombre, aPaterno, aMaterno, message) = await _apiRucDni.GetDniInfoAsync(documentNumber); if (message == "Ha superado la cantidad de consultas mensuales") { MessageBox.Show(message, "Límite de Consultas Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning); break; } _databaseService.UpdateDocumentInfo(documentNumber, "-", nombre, aPaterno, aMaterno, "-", "-", "-", "-", "-", "-", "-", "-", message, "DNI"); _loadLabel.Text = $"Consultando DNI {documentNumber} - {nombre} {aPaterno} {aMaterno}"; } else if (documentNumber.Length == 11) { var (rucInfo, direccion, estado, condicion, departamento, provincia, distrito, ubigeo, agente_rentencion, message) = await _apiRucDni.GetRucInfoAsync(documentNumber); if (message == "Ha superado la cantidad de consultas mensuales") { MessageBox.Show(message, "Límite de Consultas Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning); break; } _databaseService.UpdateDocumentInfo(documentNumber, rucInfo, "-", "-", "-", direccion, departamento, provincia, distrito, ubigeo, estado, condicion, agente_rentencion, message, "RUC"); var rucNatura = documentNumber.Substring(0, 2); if (rucNatura == "10") { var dniNumber = documentNumber.Substring(2, 8); var (nombre, aPaterno, aMaterno, dniMessage) = await _apiRucDni.GetDniInfoAsync(dniNumber); if (dniMessage == "Ha superado la cantidad de consultas mensuales") { MessageBox.Show(dniMessage, "Límite de Consultas Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning); break; } _databaseService.UpdateDocumentInfo(documentNumber, rucInfo, nombre, aPaterno, aMaterno, direccion, departamento, provincia, distrito, ubigeo, estado, condicion, agente_rentencion, dniMessage, "RUC PERSONA NATURAL"); _loadLabel.Text = $"Consultando DNI {documentNumber} - {nombre} {aPaterno} {aMaterno}"; } _loadLabel.Text = $"Consultando RUC {documentNumber} - {rucInfo}"; } else { _loadLabel.Text = $"Documento {documentNumber} inválido."; throw new ArgumentException("Número de documento inválido. Debe tener 8 o 11 dígitos."); } if (validationCounter >= 25) { string tableNamedbs = "ExcelData"; _sqliteLoader.LoadDataTableToDataGridView(tableNamedbs, _dataGridView); validationCounter = 0; } else { validationCounter++; } } catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message, "Error de Autorización", MessageBoxButtons.OK, MessageBoxIcon.Error); break; } catch (Exception ex) { MessageBox.Show($"Error al consultar el documento {documentNumber}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } }
string tableNamedb = "ExcelData";
_sqliteLoader.LoadDataTableToDataGridView(tableNamedb, _dataGridView);
_loadLabel.Visible = false;

## Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue o envía un pull request para cualquier mejora o corrección.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más detalles.
  