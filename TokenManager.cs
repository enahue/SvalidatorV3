﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Svalidator
{
    public partial class TokenManager : Form
    {
        private string databaseFile = String.Format("Data Source={0}", @"database.db");
        string consulta = "";
        public static TokenManager Instance { get; private set; }
        public TokenManager()
        {
            InitializeComponent();
            Instance = this;

            this.FormClosed += (s, e) =>
            {
                Instance = null;
            };
        }
        private void create_db(SQLiteConnection conexionSQLite)
        {
            if (!System.IO.File.Exists(@"database.db"))
            {
                try
                {
                    SQLiteConnection.CreateFile("database.db");

                    conexionSQLite.Open();
                    string consultaSQL = "CREATE TABLE IF NOT EXISTS api_token (id INTEGER PRIMARY KEY, token TEXT NOT NULL);";
                    string consultaSQL2 = "INSERT INTO api_token (token) values ('Api token not found.');";
                    SQLiteCommand comandoSQL = new SQLiteCommand(consultaSQL, conexionSQLite);
                    SQLiteCommand comandoSQL2 = new SQLiteCommand(consultaSQL2, conexionSQLite);
                    comandoSQL.CommandType = CommandType.Text;
                    comandoSQL2.CommandType = CommandType.Text;
                    comandoSQL.ExecuteNonQuery();
                    comandoSQL2.ExecuteNonQuery();
                    consultar_tabla(conexionSQLite);
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error al crear la BD SQLite. " +
                        "Error: " + error.Message,
                        "Error al crear BD SQLite",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                conexionSQLite.Open();

                consultar_tabla(conexionSQLite);

            }
            conexionSQLite.Close();

        }
        private void consultar_tabla(SQLiteConnection conexionSQLite)
        {

            string consultaSQL = "SELECT token FROM api_token WHERE id = 1";
            SQLiteCommand comandoSQL = new SQLiteCommand(consultaSQL, conexionSQLite);
            string nombreAPIClave = (string)comandoSQL.ExecuteScalar();
            consulta = nombreAPIClave;
        }


        private void TokenManager_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            using (var conexionSQLite = new SQLiteConnection(databaseFile))
            {
                create_db(conexionSQLite);
                txb_token.Text = consulta;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Registrar Token nuevo?", "Token", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                using (var conexionSQLite = new SQLiteConnection(databaseFile))
                {

                    conexionSQLite.Open();
                    string consultaSQL = "UPDATE api_token SET token = '" + txb_newtoken.Text + "' WHERE id = 1";
                    SQLiteCommand comandoSQL = new SQLiteCommand(consultaSQL, conexionSQLite);
                    comandoSQL.CommandType = CommandType.Text;
                    comandoSQL.ExecuteNonQuery();
                    txb_newtoken.Text = "";
                    consultar_tabla(conexionSQLite);
                    txb_token.Text = consulta;
                }
            }
            else
            {
                return;
            }
        }
    }
}
