using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryPedia.Interfaces;
using HistoryPedia.Models;

namespace HistoryPedia.Repositories
{
    public class ArticleRepository : IGetArticle
    {
        private readonly ArticleContext _articleContext;
        public ArticleRepository(ArticleContext articleContext)
        {
            _articleContext = articleContext;
        }
        public Article GetObjectArticle(int id) => _articleContext.Articles.FirstOrDefault(p => p.Id == id);
        public List<Article> GetAllArticles => _articleContext.Articles.ToList();
    }
}
