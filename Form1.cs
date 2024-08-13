using System.Data.SQLite;
using System.Windows.Forms;

namespace Svalidator
{

    public partial class Form1 : Form
    {

        string token = "";
        private ExcelToSQLite excelToSQLite;
        private SQLiteLoader sqliteLoader;
        string connectionString = "Data Source=database.db;Version=3;";
        private ExcelExporter excelExporter;
        public Form1()
        {
            InitializeComponent();

            excelToSQLite = new ExcelToSQLite(connectionString);
            sqliteLoader = new SQLiteLoader(connectionString);
            excelExporter = new ExcelExporter(connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                lbl_message.Visible = true;
                lbl_message.Text = "No hay datos para descargar...";
                return;
            }
            else
            {

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.FileName = "Validacion-dni-ruc-" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        string query = "SELECT DOCUMENTO AS 'DNI/RUC', RSOCIAL AS 'RAZON SOCIAL' FROM ExcelData";

                        try
                        {
                            excelExporter.ExportToExcel(query, filePath);
                            MessageBox.Show("Archivo Excel exportado con éxito.");
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void consultaSqlite(SQLiteConnection conexionSQLite)
        {
            try
            {

                string consultaSQL = "SELECT token FROM api_token WHERE id = 1";
                SQLiteCommand comandoSQL = new SQLiteCommand(consultaSQL, conexionSQLite);

                string nombreAPIClave = (string)comandoSQL.ExecuteScalar();
                token = nombreAPIClave;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al consultar la BD SQLite. " +
                    "Error: " + e.Message,
                    "Error al consultar BD SQLite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void btn_cargarexcel_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(@"database.db"))
            {
                MessageBox.Show("No se ha configurado el token de la API. Por favor, configure el token en el menú 'Configurar Token'.", "Token no configurado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                lbl_message.Visible = true;
                lbl_message.Text = "Cargando datos...";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string tableName = "ExcelData";
                    excelToSQLite.LoadExcelToSQLite(filePath, tableName);
                    lbl_message.Text = "Datos cargados correctamente.";
                    lbl_message.Visible = false;
                }
            }

            string tableNamedb = "ExcelData";
            sqliteLoader.LoadDataTableToDataGridView(tableNamedb, dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public static async Task Main(string[] args)
        {
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(@"database.db"))
            {
                MessageBox.Show("No se ha configurado el token de la API. Por favor, configure el token en el menú 'Configurar Token'.", "Token no configurado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var conexionSQLite = new SQLiteConnection(connectionString))
            {
                conexionSQLite.Open();
                consultaSqlite(conexionSQLite);

            }
            if (token == "")
            {
                MessageBox.Show("No se ha configurado el token de la API. Por favor, configure el token en el menú 'Configurar Token'.", "Token no configurado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                lbl_message.Visible = true;
                lbl_message.Text = "No hay datos para procesar...";
                return;
            }
            else
            {
                lbl_message.Visible = true;
                var databaseService = new DatabaseService(connectionString);
                var dniRucService = new ApiRucDni(token);
                var sqliteLoad = new SQLiteLoader(connectionString);
                var loadLabel = lbl_message;
                var rucProcessor = new DocumentProcessor(databaseService, dniRucService, sqliteLoad, dataGridView1, loadLabel);
                await rucProcessor.ProcessRucNumbersAsync();

            }

        }

        private void txb_loading_TextChanged(object sender, EventArgs e)
        {

        }

        private void tokenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void configurarTokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TokenManager.Instance == null)
            {
                TokenManager nuevoFormSecundario = new TokenManager();
                nuevoFormSecundario.Show();
            }
            else
            {

                TokenManager.Instance.BringToFront();
            }
        }
    }
}
