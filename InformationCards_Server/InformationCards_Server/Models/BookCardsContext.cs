using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace InformationCards_Server.Models
{
    public class BookCardsContext
    {
        private readonly string _path = 
            Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString(), "Cards/");

        public async Task<List<BookCard>> Get()
        {
            string filePath = Path.Combine(_path, "books.json");
            StreamReader sr = new StreamReader(filePath);
            string jsonString = sr.ReadToEnd();
            sr.Close();
            var list = JsonConvert.DeserializeObject<List<BookCard>>(jsonString);
            return list;
        }

        public async Task Add(BookCard bookCard)
        {
            string filePath = Path.Combine(_path, "books.json");
            StreamReader sr = new StreamReader(filePath);
            string jsonString = sr.ReadToEnd();
            sr.Close();

            var list = JsonConvert.DeserializeObject<List<BookCard>>(jsonString);
            if(list == null)
            {
                list = new List<BookCard>();
            }
            list.Add(bookCard);
            var newJson = JsonConvert.SerializeObject(list);

            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(newJson);
            sw.Close();
        }

        public async Task Remove(int id)
        {
            string filePath = Path.Combine(_path, "books.json");
            StreamReader sr = new StreamReader(filePath);
            string jsonString = sr.ReadToEnd();
            sr.Close();

            var list = JsonConvert.DeserializeObject<List<BookCard>>(jsonString);
            var cardToDelete = list.Find(x => x.Id == id);
            list.Remove(cardToDelete);
            var newJson = JsonConvert.SerializeObject(list);

            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(newJson);
            sw.Close();
        }

        public async Task Edit(BookCard bookCard)
        {
            string filePath = Path.Combine(_path, "books.json");
            StreamReader sr = new StreamReader(filePath);
            string jsonString = sr.ReadToEnd();
            sr.Close();

            var list = JsonConvert.DeserializeObject<List<BookCard>>(jsonString);
            var cardToEdit = list.Find(x => x.Id == bookCard.Id);
            var cardIndex = list.IndexOf(cardToEdit);

            list[cardIndex] = bookCard;
            var newJson = JsonConvert.SerializeObject(list);

            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(newJson);
            sw.Close();
        }
    }
}
