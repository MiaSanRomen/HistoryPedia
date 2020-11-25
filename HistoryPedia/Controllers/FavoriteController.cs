using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryPedia.Data;
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
        public FavoriteController(ApplicationContext contextApp, ArticleContext contextArt)
        {
            db = contextArt;
            dbUsers = contextApp;
        }

        public ActionResult Index()
        {
            User user = dbUsers.Users.ToList().FirstOrDefault(g => g.UserName == User.Identity.Name);
            user.FavoriteArticles = db.Articles.Where(x => x.UserId == user.Id).ToList();
            ManyToManyLink.NewUser(user.UserName);
            foreach (var item in ManyToManyLink.UsersToArticles[user.UserName])
            {
                var article = db.Articles.FirstOrDefault(x => x.Id == item.Id);
                if (user.FavoriteArticles == null)
                {
                    user.FavoriteArticles = new List<Article>() { article };
                }
                else
                {
                    user.FavoriteArticles.Add(article);
                }
            }
            ManyToManyLink.UsersToArticles.Clear();
            foreach (var item in user.FavoriteArticles)
            {
                item.Image = db.Pictures.FirstOrDefault(x => x.Name == item.ImageName);
            }

            return View(user);
        }


        public async Task<IActionResult> AddToFavorite(int id)
        {
            Article article = db.Articles.FirstOrDefault(g => g.Id == id);
            User user = dbUsers.Users.FirstOrDefault(g => g.UserName == User.Identity.Name);
            if (article != null)
            {
                article.UserId = user.Id;
                ManyToManyLink.NewLink(user.UserName, article);
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(Article article)
        //{
        //    db.Articles.Update(article);
        //    if (string.IsNullOrEmpty(article.ImageName))
        //    {
        //        article.ImageName = "DefIco";
        //    }
        //    article.Image = db.Pictures.FirstOrDefault(x => x.Name == article.ImageName);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //public RedirectToRouteResult RemoveFromFavorite(int gameId, string returnUrl)
        //{
        //    Game game = repository.Games
        //        .FirstOrDefault(g => g.GameId == gameId);

        //    if (game != null)
        //    {
        //        GetCart().RemoveLine(game);
        //    }
        //    return RedirectToAction("Index", new { returnUrl });
        //}

        //public Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}
    }
}
