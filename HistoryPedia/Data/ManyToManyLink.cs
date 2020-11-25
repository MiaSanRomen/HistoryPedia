using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        public static void NewUser(string userName)
        {
            if (!UsersToArticles.ContainsKey(userName))
            {
                UsersToArticles.Add(userName, new List<Article>());
            }
        }
    }
}
