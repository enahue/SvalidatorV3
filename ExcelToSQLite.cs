using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using SpreadsheetLight;

public class ExcelToSQLite
{
    private string connectionString;

    public ExcelToSQLite(string sqliteConnectionString)
    {
        connectionString = sqliteConnectionString;
    }

    public void LoadExcelToSQLite(string filePath, string tableName)
    {
        if (!File.Exists(filePath))
        {
            MessageBox.Show("El archivo no existe.");
            return;
        }

        DataTable dataTable = new DataTable();

        using (SLDocument sl = new SLDocument(filePath))
        {
            SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
            int rowCount = stats.EndRowIndex;
            int colCount = stats.EndColumnIndex;

            // Crear las columnas en el DataTable
            for (int col = 1; col <= colCount; col++)
            {
                dataTable.Columns.Add(sl.GetCellValueAsString(1, col));
            }

            // Llenar el DataTable con los datos del Excel
            for (int row = 2; row <= rowCount; row++)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int col = 1; col <= colCount; col++)
                {
                    dataRow[col - 1] = sl.GetCellValueAsString(row, col);
                }
                dataTable.Rows.Add(dataRow);
            }
        }

        // Guardar el DataTable en la base de datos SQLite
        SaveDataTableToSQLite(dataTable, tableName);
    }

    private void SaveDataTableToSQLite(DataTable dataTable, string tableName)
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string dropTableQuery = "DROP TABLE IF EXISTS " + tableName;
            // Crear la tabla si no existe
            string createTableQuery = "CREATE TABLE IF NOT EXISTS " + tableName + " ( ID INTEGER PRIMARY KEY AUTOINCREMENT, DOCUMENTO TEXT, RSOCIAL TEXT, NOMBRES TEXT, APATERNO TEXT, AMATERNO TEXT,  DIRECCION TEXT, UBIGEO TEXT, ESTADO TEXT, API TEXT, TIPODOC TEXT";
           
            using (SQLiteCommand cmd = new SQLiteCommand(dropTableQuery, conn))
            {
                cmd.ExecuteNonQuery();
            }
            createTableQuery = createTableQuery.TrimEnd(',', ' ') + ")";
            using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
            {
                cmd.ExecuteNonQuery();
            }


                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = $"INSERT INTO {tableName} (DOCUMENTO) VALUES (@DOCUMENTO)";

                        foreach (DataRow row in dataTable.Rows)
                        {
                            cmd.Parameters.AddWithValue("@DOCUMENTO", row[0].ToString());
                            //cmd.Parameters.AddWithValue("@DNI", row[1].ToString());
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                    }
                    transaction.Commit();
                }
            }
    }
}