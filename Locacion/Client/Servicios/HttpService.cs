using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Locacion.Client.Servicios
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient http;

        public HttpService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<HttpRespuesta<T>> Get<T>(string url)
        {
            var respuestaHttp = await http.GetAsync(url);
            if (respuestaHttp.IsSuccessStatusCode)
            {
                var respuesta = await DeserializarRespuesta<T>(respuestaHttp);
                return new HttpRespuesta<T>(respuesta,
                                            false,
                                            respuestaHttp);
            }
            else
            {
                return new HttpRespuesta<T>(default,
                                            true,
                                            respuestaHttp);
            }
        }

        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpRespuesta)
        {
            var RepuestaString = await httpRespuesta.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(RepuestaString,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
