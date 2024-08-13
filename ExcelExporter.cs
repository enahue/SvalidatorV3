using System;
using System.Data;
using System.Data.SQLite;
using ClosedXML.Excel;

public class ExcelExporter
{
    private readonly string _connectionString;

    public ExcelExporter(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void ExportToExcel(string query, string filePath)
    {
        try
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add(dataTable, "Sheet1");
                            workbook.SaveAs(filePath);
                        }
                    }
                }
            }
        }
        catch (IOException ex)
        {
            throw new IOException("El archivo está en uso por otro proceso. Por favor, ciérralo e inténtalo de nuevo.", ex);
        }
    }
}
