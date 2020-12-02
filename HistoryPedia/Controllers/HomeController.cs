using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HistoryPedia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web;
using HistoryPedia.signalR;
using Microsoft.AspNetCore.SignalR;

namespace HistoryPedia.Controllers
{
    public class HomeController : Controller
    {

        private ArticleContext db;
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext dbUsers;
        IHubContext<ChatHub> hubContext;

        public HomeController(ILogger<HomeController> logger, ApplicationContext contextApp, ArticleContext contextArt, IHubContext<ChatHub> hubContext)
        {
            db = contextArt;
            dbUsers = contextApp;
            _logger = logger;
            this.hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article article)
        {
            db.Articles.Add(article);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Article article = await db.Articles.FirstOrDefaultAsync(p => p.Id == id);
                article.Blocks = db.BlocksInfo.Where(x => x.ArticleId == article.Id).ToList();
                var Pictures = db.Pictures;
                article.Image = Pictures.FirstOrDefault(x => x.Name == article.ImageName);
                foreach (var item in article.Blocks)
                {
                    if (item.BlockImageName == null)
                        item.Image = Pictures.FirstOrDefault(x => x.Name == "Def1");
                    else
                        item.Image = Pictures.FirstOrDefault(x => x.Name == item.BlockImageName);
                }
                if (article != null)
                    return View(article);
            }

            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != 0)
            {
                Article article = await db.Articles.FirstOrDefaultAsync(p => p.Id == id);
                if (article != null)
                {
                    article.Blocks = db.BlocksInfo.Where(x => x.ArticleId == article.Id).ToList();
                    return View(article);
                }
            }
            else
            {
                Article article = new Article();
                db.Articles.Add(article);
                await db.SaveChangesAsync();
                return View(article);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Article article)
        {
            db.Articles.Update(article);
            if (string.IsNullOrEmpty(article.ImageName))
            {
                article.ImageName = "DefIco";
            }
            article.Image = db.Pictures.FirstOrDefault(x => x.Name == article.ImageName);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Article article = await db.Articles.FirstOrDefaultAsync(p => p.Id == id);
                if (article != null)
                    return View(article);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Article article = new Article {Id = id.Value};
                db.Entry(article).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public ActionResult Index(string name)
        {
            var articles = string.IsNullOrEmpty(name)? db.Articles.ToList() : db.Articles.Where(p => p.Name.Contains(name)).ToList();
            foreach (var item in articles)
            {
                item.Image = db.Pictures.FirstOrDefault(x => x.Name == item.ImageName);
            }
            User user = dbUsers.Users.ToList().FirstOrDefault(g => g.UserName == User.Identity.Name);

            SearchArticle dataArticles = new SearchArticle();
            dataArticles.Articles = articles;
            dataArticles.User = user;
            db.SaveChanges();
            return View(dataArticles);
        }

        [HttpGet, ActionName("DeleteBlock")]
        public async Task<IActionResult> ConfirmDeleteBlock(int? idBlock)
        {
            if (idBlock != null)
            {
                BlockInfo blockInfo = await db.BlocksInfo.FirstOrDefaultAsync(p => p.BlockInfoId == idBlock);
                if (blockInfo != null)
                    return View(blockInfo);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlock(int? idBlock)
        {
            if (idBlock != null)
            {
                BlockInfo blockInfo = await db.BlocksInfo.FirstOrDefaultAsync(p => p.BlockInfoId == idBlock);
                if (blockInfo != null)
                {
                    db.BlocksInfo.Remove(blockInfo);
                    await db.SaveChangesAsync();
                    //return RedirectToAction("Edit", "Home", blockInfo.ArticleId);
                    Article article = await db.Articles.FirstOrDefaultAsync(p => p.Id == blockInfo.ArticleId);
                    article.Blocks = db.BlocksInfo.Where(x => x.ArticleId == article.Id).ToList();
                    return View("Edit", article);
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> EditBlock(int? idBlock, int id)
        {
            if (idBlock != null)
            {
                BlockInfo blockInfo = await db.BlocksInfo.FirstOrDefaultAsync(p => p.BlockInfoId == idBlock);
                if (blockInfo != null)
                {
                    blockInfo.PrevId = blockInfo.BlockInfoId;
                    return View(blockInfo);
                }
            }
            else
            {
                BlockInfo blockInfo = new BlockInfo();
                blockInfo.ArticleId = id;
                db.BlocksInfo.Add(blockInfo);
                await db.SaveChangesAsync();
                blockInfo.PrevId = db.BlocksInfo.ToList().Last().BlockInfoId;
                return View(blockInfo);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditBlock(BlockInfo blockInfo)
        {
            db.BlocksInfo.Update(blockInfo);
            if (string.IsNullOrEmpty(blockInfo.BlockImageName))
            {
                blockInfo.BlockImageName = "Def1";
            }
            blockInfo.Image = db.Pictures.FirstOrDefault(x => x.Name == blockInfo.BlockImageName);
            await db.SaveChangesAsync();
            var deleteBlock = db.BlocksInfo.FirstOrDefault(x => x.BlockInfoId == blockInfo.PrevId);
            if (deleteBlock!= null)
            {
                db.BlocksInfo.Remove(deleteBlock);
            }
            await db.SaveChangesAsync();
            Article article = await db.Articles.FirstOrDefaultAsync(p => p.Id == blockInfo.ArticleId);
            article.Blocks = db.BlocksInfo.Where(x => x.ArticleId == article.Id).ToList();
            return View("Edit", article);
        }

        //[HttpPost]
        //public ActionResult Create(Picture pic, HttpPostedFileBase uploadImage)
        //{
        //    if (ModelState.IsValid && uploadImage != null)
        //    {
        //        byte[] imageData = null;
        //        // считываем переданный файл в массив байтов
        //        using (var binaryReader = new BinaryReader(uploadImage.InputStream))
        //        {
        //            imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
        //        }
        //        // установка массива байтов
        //        pic.Image = imageData;

        //        db.Pictures.Add(pic);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    return View(pic);
        //}

        [Authorize]
        public IActionResult Chat()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SendChat(string message)
        {
            await hubContext.Clients.All.SendAsync("Send", $"{User.Identity.Name}: {message} - {DateTime.Now.ToShortTimeString()}");
            return RedirectToAction("Index");
        }
    }
}
