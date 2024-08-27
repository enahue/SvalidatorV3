using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Newtonsoft.Json.Linq;

public class ApiRucDni
{
    private readonly HttpClient _httpClient;
    private readonly string _token;


    public ApiRucDni(string token)
    {
        _httpClient = new HttpClient();
        _token = token;
        ConfigureHttpClient(_token);
    }
    private void ConfigureHttpClient(string token)
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<(string RazonSocial, string Direccion, string Estado, string Condicion, string Departamento, string Provincia, string Distrito, string AgenteRetencion, string Ubigeo, string message)> GetRucInfoAsync(string ruc)
    {
        var url = "https://apiperu.dev/api/ruc";
        var content = new StringContent($"{{\"ruc\":\"{ruc}\"}}", Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException("Token no autorizado o expirado.");
        }
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error al consultar el RUC: {response.ReasonPhrase}");
        }
        var responseContent = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(responseContent);
        var message = json["message"]?.ToString() ?? "Consultado";
        var errorMessage = "";

        if (message == "Ha superado la cantidad de consultas mensuales")
        {
            throw new Exception(message);
        }
        var data = json["data"];

        var razonSocial = data?["nombre_o_razon_social"]?.ToString() ?? "-";
        var direccion = data?["direccion"]?.ToString() ?? "-";
        var estado = data?["estado"]?.ToString() ?? "-";
        var condicion = data?["condicion"]?.ToString() ?? "-";
        var departamento = data?["departamento"]?.ToString() ?? "-";
        var provincia = data?["provincia"]?.ToString() ?? "-";
        var distrito = data?["distrito"]?.ToString() ?? "-";
        var ubigeo = data?["ubigeo_sunat"]?.ToString() ?? "-";
        var agente_rentencion = data?["es_agente_de_retencion"]?.ToString() ?? "-";
       
        var success = json["success"]?.ToString();


        return (razonSocial, direccion, estado, condicion, departamento, provincia, distrito, ubigeo, agente_rentencion, message);
    }

    public async Task<(string Nombre, string Apaterno, string Amaterno, string message)> GetDniInfoAsync(string dni)
    {
        var url = "https://apiperu.dev/api/dni";
        var content = new StringContent($"{{\"dni\":\"{dni}\"}}", Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);



        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException("Token no autorizado o expirado.");
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error al consultar el DNI: {response.ReasonPhrase}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(responseContent);
        var success = json["success"]?.ToObject<bool>() ?? false;
        var message = json["message"]?.ToString() ?? "Consultado";


        var data = json["data"];

        if (message == "Ha superado la cantidad de consultas mensuales")
        {
            throw new Exception(message);
        }
        //if (data == null)
        //{
        //    throw new Exception("La respuesta no contiene datos.");
        //    //throw new Exception(message);
        //}

        var nombre = data?["nombres"]?.ToString() ?? "-";
        var aPaterno = data?["apellido_paterno"]?.ToString() ?? "-";
        var aMaterno = data?["apellido_materno"]?.ToString() ?? "-";

        
        
        return (nombre, aPaterno, aMaterno, message);
    }

    public async Task<string> GetDocumentInfoAsync(string documentNumber)
    {
        if (documentNumber.Length == 8)
        {
            var (nombre, aPaterno, aMaterno, message) = await GetDniInfoAsync(documentNumber);
            return $"Nombre: {nombre}, Apaterno: {aPaterno}, Amaterno: {aPaterno}";
        }
        else if (documentNumber.Length == 11)
        {
            var rucNatura = documentNumber.Substring(0, 2);
            var (razonSocial, direccion, estado, condicion, departamento, provincia, distrito, arentencion, ubigeo, message) = await GetRucInfoAsync(documentNumber);

            if (rucNatura.ToString() == "10")
            {
                var dniNumber = documentNumber.Substring(3, 8);
                var (nombre, aPaterno, aMaterno, messages) = await GetDniInfoAsync(dniNumber);
                return $"Nombre: {nombre}, Apaterno: {aPaterno}, Amaterno: {aPaterno}";
            }
            return $"RazonSocial: {razonSocial}, Direccion: {direccion}, Departamento: {departamento}, Estado: {estado}, Ubigeo: {ubigeo}";
        }
        else
        {
            throw new ArgumentException("Número de documento inválido. Debe tener 8 o 11 dígitos.");
        }
    }
}
