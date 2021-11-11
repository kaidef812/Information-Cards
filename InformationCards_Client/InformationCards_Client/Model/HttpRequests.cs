using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            catch (HttpRequestException e)
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
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await _client.PostAsync("https://localhost:44309/api/bookCards", httpContent);
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }

        public async Task PutCard(BookCard cardToChange)
        {
            try
            {
                string jsonCardToChange = JsonConvert.SerializeObject(cardToChange);
                HttpContent httpContent = new StringContent(jsonCardToChange);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await _client.PutAsync("https://localhost:44309/api/bookCards", httpContent);
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }

        public async Task DeleteCard(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync("https://localhost:44309/api/bookCards?id=" + id.ToString());
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }
    }
}
