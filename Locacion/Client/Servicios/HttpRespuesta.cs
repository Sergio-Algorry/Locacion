using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Locacion.Client.Servicios
{
    public class HttpRespuesta<T>
    {
        public T Respuesta { get; }
        public bool Error { get; }
        public HttpResponseMessage httpResponseMessage { get; }

        public HttpRespuesta(T respuesta,
                             bool error,
                             HttpResponseMessage httpResponseMessage)
        {
            Respuesta = respuesta;
            Error = error;
            this.httpResponseMessage = httpResponseMessage;
        }

        public async Task<string> GetBody()
        {
            var resp = await httpResponseMessage.Content.ReadAsStringAsync();
            return resp;
        }

    }
}
