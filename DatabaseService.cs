using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

public class DatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<string> GetRucNumbers()
    {
        var rucNumbers = new List<string>();

        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand("select DOCUMENTO FROM ExcelData WHERE trim(DOCUMENTO) != '' AND API != 'Consultado' order by ID;", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rucNumbers.Add(reader["DOCUMENTO"].ToString());
                    }
                }
            }
        }

        return rucNumbers;
    }

    public void UpdateDocumentInfo(string documento, string razonSocial, string nombre, string aPaterno,string aMaterno,string direccion, string departamento, string provincia,string distrito, string ubigeo, string estado,string condicion, string aRetencion, string message, string tipoDoc)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand("UPDATE ExcelData SET RSOCIAL = @RazonSocial, NOMBRES = @Nombres, APATERNO = @Apaterno,AMATERNO = @Amaterno, DIRECCION = @Direccion, DEPARTAMENTO = @Departamento, PROVINCIA = @Provincia, DISTRITO = @Distrito,  UBIGEO = @Ubigeo, ESTADO = @Estado, CONDICION = @Condicion, ARETENCION = @ARetencion, API = @Message, TIPODOC = @TipoDoc WHERE DOCUMENTO = @DOCUMENTO", connection))
            {
                command.Parameters.AddWithValue("@RazonSocial", razonSocial);
                command.Parameters.AddWithValue("@DOCUMENTO", documento);
                command.Parameters.AddWithValue("@Direccion", direccion);
                command.Parameters.AddWithValue("@Departamento", departamento);
                command.Parameters.AddWithValue("@Provincia", provincia);
                command.Parameters.AddWithValue("@Distrito", distrito);
                command.Parameters.AddWithValue("@Estado", estado);
                command.Parameters.AddWithValue("@Condicion", condicion);
                command.Parameters.AddWithValue("@ARetencion", aRetencion);
                command.Parameters.AddWithValue("@Ubigeo", ubigeo);
                command.Parameters.AddWithValue("@Nombres", nombre);
                command.Parameters.AddWithValue("@Apaterno", aPaterno);
                command.Parameters.AddWithValue("@Amaterno", aMaterno);
                command.Parameters.AddWithValue("@TipoDoc", tipoDoc);
                command.Parameters.AddWithValue("@Message", message);
                command.ExecuteNonQuery();


            }
        }
    }
}
