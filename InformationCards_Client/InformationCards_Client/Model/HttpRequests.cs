using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace InformationCards_Client.Model
{
    class HttpRequests
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<List<BookCard>> GetCards()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("https://localhost:44309/api/bookCards");

                HttpContent content = response.Content;

                var json = await content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<BookCard>>(json);

                return result;
            }
            catch(HttpRequestException e)
            {
                throw e;
            }
        }

        public async Task PostCard(BookCard newCard)
        {
            try
            {
                string jsonNewCard = JsonConvert.SerializeObject(newCard);
                HttpContent httpContent = new StringContent(jsonNewCard);
                HttpResponseMessage response = await _client.PostAsync("https://localhost:44309/api/bookCards", httpContent);
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }
    }
}
