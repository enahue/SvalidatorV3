using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Svalidator;

public class DocumentProcessor
{
    private readonly DatabaseService _databaseService;
    private readonly ApiRucDni _apiRucDni;
    private readonly SQLiteLoader _sqliteLoader;
    private readonly DataGridView _dataGridView;
    private readonly Label _loadLabel;

    public DocumentProcessor(DatabaseService databaseService, ApiRucDni dniRucService, SQLiteLoader sqliteLoader, DataGridView dataGridView, Label loadLabel)
    {
        _databaseService = databaseService;
        _apiRucDni = dniRucService;
        _sqliteLoader = sqliteLoader;
        _dataGridView = dataGridView;
        _loadLabel = loadLabel;
    }

    public async Task ProcessRucNumbersAsync()
    {
        var documentNumbers = _databaseService.GetRucNumbers();
        int validationCounter = 0;
        foreach (var documentNumber in documentNumbers)
        {


            try
            {
                if (documentNumber.Length == 8)
                {
                    var (nombre, aPaterno, aMaterno, message) = await _apiRucDni.GetDniInfoAsync(documentNumber);
                    _loadLabel.Text = message;


                    _databaseService.UpdateDocumentInfo(documentNumber, "-", nombre, aPaterno, aMaterno, "-", "-", "-", "-", "-", "-", "-", "-", message, "DNI");
                    _loadLabel.Text = $"Consultando DNI {documentNumber} - {nombre} {aPaterno} {aMaterno}";
                }
                else if (documentNumber.Length == 11)
                {

                    var (rucInfo, direccion, estado, condicion, departamento, provincia, distrito, ubigeo, agente_rentencion, message) = await _apiRucDni.GetRucInfoAsync(documentNumber);
                    if (message == "Ha superado la cantidad de consultas mensuales")
                    {
                        MessageBox.Show(message, "Límite de Consultas Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    _databaseService.UpdateDocumentInfo(documentNumber, rucInfo, "-", "-", "-", direccion, departamento, provincia, distrito, ubigeo, estado, condicion, agente_rentencion, message, "RUC");
                    var rucNatura = documentNumber.Substring(0, 2);
                    if (rucNatura.ToString() == "10")
                    {
                        var dniNumber = documentNumber.Substring(2, 8);
                        if (message == "Ha superado la cantidad de consultas mensuales")
                        {
                            MessageBox.Show(message, "Límite de Consultas Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                        var (nombre, aPaterno, aMaterno, messages) = await _apiRucDni.GetDniInfoAsync(dniNumber);
                        _databaseService.UpdateDocumentInfo(documentNumber, rucInfo, nombre, aPaterno, aMaterno, direccion, departamento, provincia, distrito, ubigeo, estado, condicion, agente_rentencion, message, "RUC PERSONA NATURAL");
                        _loadLabel.Text = $"Consultando DNI {documentNumber} - {nombre} {aPaterno} {aMaterno}";
                    }
                    _loadLabel.Text = $"Consultando RUC {documentNumber} - {rucInfo}";
                }
                else
                {
                    _loadLabel.Text = $"Documento {documentNumber} inválido.";
                    throw new ArgumentException("Número de documento inválido. Debe tener 8 o 11 dígitos.");
                }
                if (validationCounter >= 25)
                {
                    string tableNamedbs = "ExcelData";
                    _sqliteLoader.LoadDataTableToDataGridView(tableNamedbs, _dataGridView);
                    validationCounter = 0;
                }
                else
                {
                    validationCounter++;
                }
                // string tableNamedb = "ExcelData";
                // _sqliteLoader.LoadDataTableToDataGridView(tableNamedb, _dataGridView);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Error de Autorización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Limite de licencia superada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
            } 
 
           
        }

        string tableNamedb = "ExcelData";
        _sqliteLoader.LoadDataTableToDataGridView(tableNamedb, _dataGridView);
        _loadLabel.Visible = false;
        
    }
}
