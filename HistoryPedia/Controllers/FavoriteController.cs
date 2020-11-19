using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (user.FavoriteArticles != null)
            {
                foreach (var item in user.FavoriteArticles)
                {
                    item.Image = db.Images.FirstOrDefault(x => x.Name == item.ImageName);
                }
            }
            return View(user);
        }

        public async Task<RedirectToActionResult> AddToFavorite(int articleId, string returnUrl)
        {
            Article article = db.Articles.FirstOrDefault(g => g.Id == articleId);
            db.Articles.Update(article);
            if (article != null)
            {
                User user = dbUsers.Users.FirstOrDefault(g => g.UserName == User.Identity.Name);
                user.FavoriteArticles.Add(article);
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Home", new { returnUrl });
        }

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
