using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryPedia.Interfaces;
using HistoryPedia.Models;

namespace HistoryPedia.Repositories
{
    public class FavoriteRepository : IGetFavorite
    {
        private readonly ArticleContext _articleContext;
        public FavoriteRepository(ArticleContext articleContext)
        {
            _articleContext = articleContext;
        }
        public List<Article> GetFavorite(string id) => _articleContext.Articles.Where(x => x.UserId == id).ToList().ToList();
    }
}
