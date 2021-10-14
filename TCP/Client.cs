using Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TCP
{
    class Client
    {

        public async Task<string> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                    client.GetAsync("https://localhost:44333/api/Books");
                return await response.Content.ReadAsStringAsync();

            }
        }
        public async Task<string> Get(string isbn)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                       client.GetAsync("https://localhost:44333/api/Books/" + isbn);
                 return await response.Content.ReadAsStringAsync();
            }
        }
        public async void Post(string book)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(book, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await
                    client.PostAsync("https://localhost:44333/api/Books/", content);
            }
        }
        public async void Put(string book)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(book, Encoding.UTF8, "application/json");
                var ISBN = JsonConvert.DeserializeObject<Book>(book).ISBN13;
                HttpResponseMessage response = await
                    client.PutAsync("https://localhost:44333/api/Books/" + ISBN, content);
            }
        }
        public async void Remove(string isbn)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                    client.DeleteAsync("https://localhost:44333/api/Books/" + isbn);

            }
        }
    }
}
