using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HistoryPedia.Models;

namespace HistoryPedia.Data
{
    public static class ManyToManyLink
    {
        public static Dictionary<string, List<Article>> UsersToArticles = new Dictionary<string, List<Article>>();

        public static void NewLink(string userName, Article article)
        {
            NewUser(userName);
            if (!UsersToArticles[userName].Contains(article))
            {
                UsersToArticles[userName].Add(article);
            }
            Save();
        }

        public static void NewUser(string userName)
        {
            Load();
            if (!UsersToArticles.ContainsKey(userName))
            {
                UsersToArticles.Add(userName, new List<Article>());
            }
        }

        public static async void Save()
        {
            using (FileStream fs = new FileStream("link.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, UsersToArticles);
            }
        }

        public static async void Load()
        {
            using (FileStream fs = new FileStream("link.json", FileMode.OpenOrCreate))
            {
                UsersToArticles = await JsonSerializer.DeserializeAsync<Dictionary<string, List<Article>>>(fs);
            }
        }
    }
}
