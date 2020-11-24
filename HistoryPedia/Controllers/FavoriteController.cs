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

        [Authorize]
        public async Task<IActionResult> AddToFavorite(int id)
        {
            Article article = db.Articles.FirstOrDefault(g => g.Id == id);
            User user = dbUsers.Users.FirstOrDefault(g => g.UserName == User.Identity.Name);
            dbUsers.Users.Update(user);
            if (article != null)
            {
                if (user.FavoriteArticles == null)
                {
                    user.FavoriteArticles = new List<Article>() {article};
                }
                else
                {
                    user.FavoriteArticles.Add(article);
                }
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
