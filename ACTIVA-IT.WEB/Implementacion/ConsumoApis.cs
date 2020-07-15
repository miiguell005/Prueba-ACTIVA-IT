using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ACTIVA_IT.WEB.Implementacion.ApiAlbums
{
    public class ConsumoApis
    {

        /// <summary>
        /// Consulta la api
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetApi(string url)
        {
            using (var client  = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsStringAsync().Result;

                else
                    return null;
            }
        }

    }
}
