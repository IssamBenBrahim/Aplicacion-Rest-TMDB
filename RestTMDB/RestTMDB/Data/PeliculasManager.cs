using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestTMDB.Data
{
    class PeliculasManager
    {
        public static readonly string apikey = "34c6606ba0b79146cfef27a886248445";

        public async Task<Root> GetAll(string titulo)
        {
            string Url = "https://api.themoviedb.org/3/search/movie?api_key=" + apikey + "&query=" + titulo;

            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<Root>(result);

        }
    }
}
