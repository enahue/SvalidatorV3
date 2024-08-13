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

                string query = $"SELECT * FROM {tableName}";
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