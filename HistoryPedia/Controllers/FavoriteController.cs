using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryPedia.Data;
using HistoryPedia.Interfaces;
using HistoryPedia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HistoryPedia.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private ArticleContext db;
        private ApplicationContext dbUsers;
        private readonly IGetFavorite _getFavorite;
        private readonly IGetArticle _getArticle;
        public FavoriteController(ApplicationContext contextApp, ArticleContext contextArt, IGetFavorite getFavorite, IGetArticle getArticle)
        {
            db = contextArt;
            dbUsers = contextApp;
            _getFavorite = getFavorite;
            _getArticle = getArticle;
        }

        public ActionResult Index()
        {
            User user = dbUsers.Users.ToList().FirstOrDefault(g => g.UserName == User.Identity.Name);
            user.FavoriteArticles = _getFavorite.GetFavorite(user.Id);
            
            if (user.FavoriteArticles != null)
            {
                foreach (var item in user.FavoriteArticles)
                {
                    item.Image = db.Pictures.FirstOrDefault(x => x.PictureName == item.ImageName);
                }
            }
            db.SaveChanges();
            return View(user);
        }


        public async Task<IActionResult> AddToFavorite(int id)
        {
            Article article = db.Articles.FirstOrDefault(g => g.Id == id);
            User user = dbUsers.Users.FirstOrDefault(g => g.UserName == User.Identity.Name);
            if (article != null)
            {
                article.UserId = user.Id;
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromFavorite(int id)
        {
            Article article = db.Articles.FirstOrDefault(g => g.Id == id);
            User user = dbUsers.Users.FirstOrDefault(g => g.UserName == User.Identity.Name);
            user.FavoriteArticles = db.Articles.Where(x => x.UserId == user.Id).ToList();
            if (article != null)
            {
                user.FavoriteArticles.Remove(article);
                article.UserId = null;
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
