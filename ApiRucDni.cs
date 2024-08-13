using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class ApiRucDni
{
    private readonly HttpClient _httpClient;
    private readonly string _token;

    public ApiRucDni(string token)
    {
        _httpClient = new HttpClient();
        _token = token;
    }

    public async Task<(string RazonSocial, string Direccion, string Estado, string Ubigeo, string message)> GetRucInfoAsync(string ruc)
    {
        var url = $"https://dniruc.apisperu.com/api/v1/ruc/{ruc}?token={_token}";
        var response = await _httpClient.GetAsync(url);
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException("Token no autorizado o expirado.");
        }
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error al consultar el RUC: {response.ReasonPhrase}");
        }

        var content = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(content);
        var razonSocial = json["razonSocial"]?.ToString() ?? "-";
        var direccion = json["direccion"]?.ToString() ?? "-";
        var estado = json["estado"]?.ToString() ?? "-";
        var ubigeo = json["ubigeo"]?.ToString() ?? "-";
        var message = json["message"]?.ToString() ?? "Consultado";
        return (razonSocial, direccion, estado, ubigeo, message);
    }

    public async Task<(string Nombre, string Apaterno, string Amaterno, string message)> GetDniInfoAsync(string dni)
    {
        var url = $"https://dniruc.apisperu.com/api/v1/dni/{dni}?token={_token}";
        var response = await _httpClient.GetAsync(url);



        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException("Token no autorizado o expirado.");
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error al consultar el DNI: {response.ReasonPhrase}");
        }


        var content = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(content);
        var nombre = json["nombres"]?.ToString() ?? "-";
        var aPaterno = json["apellidoPaterno"]?.ToString() ?? "-";
        var aMaterno = json["apellidoMaterno"]?.ToString() ?? "-";
        var message = json["message"]?.ToString() ?? "Consultado";
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
            var (razonSocial, direccion, estado, ubigeo, message) = await GetRucInfoAsync(documentNumber);

            if (rucNatura.ToString() == "10")
            {
                var dniNumber = documentNumber.Substring(3, 8);
                var (nombre, aPaterno, aMaterno, messages) = await GetDniInfoAsync(documentNumber);
                return $"Nombre: {nombre}, Apaterno: {aPaterno}, Amaterno: {aPaterno}";
            }
            return $"RazonSocial: {razonSocial}, Direccion: {direccion}, Estado: {estado}, Ubigeo: {ubigeo}";
        }
        else
        {
            throw new ArgumentException("Número de documento inválido. Debe tener 8 o 11 dígitos.");
        }
    }
}
