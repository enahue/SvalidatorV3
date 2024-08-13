using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

public class SQLiteLoader
{
    private string connectionString;

    public SQLiteLoader(string sqliteConnectionString)
    {
        connectionString = sqliteConnectionString;
    }

    public void LoadDataTableToDataGridView(string tableName, DataGridView dataGridView)
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            try
            {
                conn.Open();


                string query = $" SELECT ID as 'NUMERO', DOCUMENTO, RSOCIAL AS 'RAZON SOCIAL', NOMBRES, APATERNO AS 'AP. PATERNO', AMATERNO AS 'AP. MATERNO',  DIRECCION, DEPARTAMENTO, PROVINCIA, DISTRITO, UBIGEO, ESTADO, CONDICION, ARETENCION AS 'AG. RENTENCION', API AS 'RESPUESTA API', TIPODOC AS 'TIPO DOC.' FROM {tableName}";
                //string query = $"SELECT * FROM {tableName}";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Asignar el DataTable al DataGridView
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de la tabla SQLite: {ex.Message}");
            }
        }
    }
}