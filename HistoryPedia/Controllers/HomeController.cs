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
using HistoryPedia.Data;
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

        

        [Authorize(Roles = "admin")]
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
                item.Blocks = db.BlocksInfo.Where(x => x.ArticleId == item.Id).ToList();

                foreach (var block in item.Blocks)
                {
                    block.ArticleName = item.Name;
                }
            }
            User user = dbUsers.Users.ToList().FirstOrDefault(g => g.UserName == User.Identity.Name);

            SearchArticle dataArticles = new SearchArticle();
            dataArticles.Articles = articles;
            dataArticles.User = user;
            db.SaveChanges();
            return View(dataArticles);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != 0)
            {
                Article article = await db.Articles.FirstOrDefaultAsync(p => p.Id == id);
                if (article != null)
                {
                    article.Blocks = db.BlocksInfo.Where(x => x.ArticleId == article.Id).ToList();
                    DataClass.TempArticle = article;
                    return View(article);
                }
            }
            else
            {
                Article article = new Article();
                article.Name = "New article";
                article.Blocks = DataClass.TempList;
                DataClass.TempArticle = article;
                return View(article);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Article article)
        {
            NewArticle(article);

            if (string.IsNullOrEmpty(article.ImageName))
            {
                article.ImageName = "DefIco";
            }
            article.Image = db.Pictures.FirstOrDefault(x => x.Name == article.ImageName);

            if (DataClass.TempList.Count != 0)
            {
                foreach (var item in DataClass.TempList)
                {
                    //item.BlockInfoId = db.BlocksInfo.Last().BlockInfoId;
                    item.ArticleId = article.Id;
                    //db.BlocksInfo.Add(item);
                    //await db.SaveChangesAsync();
                    NewBlock(item);
                }
                DataClass.TempList.Clear();
            }

            var blocks = db.BlocksInfo.Where(x => x.ArticleId == article.Id).ToList();
            foreach (var item in blocks)
            {
                article.Blocks.Add(item);
            }

            if (article.Blocks != null)
            {
                foreach (var item in article.Blocks)
                {
                    item.ArticleId = article.Id;
                }

            }
            await db.SaveChangesAsync();
            DataClass.TempArticle = null;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void NewArticle(Article article)
        {
            db.Articles.Update(article);
            db.SaveChanges();
        }


        [HttpPost]
        public void NewBlock(BlockInfo blockInfo)
        {
            db.BlocksInfo.Update(blockInfo);
            db.SaveChanges();
        }

        [HttpGet, ActionName("DeleteBlock")]
        public async Task<IActionResult> ConfirmDeleteBlock(int idBlock, string name)
        {
            if (idBlock != 0)
            {
                BlockInfo blockInfo = await db.BlocksInfo.FirstOrDefaultAsync(p => p.BlockInfoId == idBlock);
                if (blockInfo != null)
                    return View(blockInfo);
            }
            else
            {
                var block = DataClass.TempList.FirstOrDefault(p => p.BlockName == name);
                return View(block);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlock(int idBlock, string name)
        {
            if (idBlock != 0)
            {
                BlockInfo blockInfo = await db.BlocksInfo.FirstOrDefaultAsync(p => p.BlockInfoId == idBlock);
                if (blockInfo != null)
                {
                    db.BlocksInfo.Remove(blockInfo);
                    await db.SaveChangesAsync();
                    Article article = await db.Articles.FirstOrDefaultAsync(p => p.Id == blockInfo.ArticleId);
                    article.Blocks = db.BlocksInfo.Where(x => x.ArticleId == article.Id).ToList();
                    return View("Edit", article);
                }
            }
            else
            {
                var block = DataClass.TempList.FirstOrDefault(p => p.BlockName == name);
                DataClass.TempList.Remove(block);
                DataClass.TempArticle.Blocks.Remove(block);
                return View("Edit", DataClass.TempArticle);
            }
            return NotFound();
        }

        public async Task<IActionResult> EditBlock(int idBlock, string blockName)
        {
            if (idBlock != 0)
            {
                BlockInfo blockInfo = await db.BlocksInfo.FirstOrDefaultAsync(p => p.BlockInfoId == idBlock);
                if (blockInfo != null)
                {
                    //blockInfo.PrevId = blockInfo.BlockInfoId;
                    return View(blockInfo);
                }
            }
            else
            {
                if (blockName != null)
                {
                    BlockInfo blockInfo = DataClass.TempList.FirstOrDefault(p => p.BlockName == blockName);
                    blockInfo.PrevName = blockInfo.BlockName;
                    return View(blockInfo);
                }
                else
                {
                    BlockInfo blockInfo = new BlockInfo();
                    blockInfo.ArticleName = DataClass.TempArticle.Name;
                    return View(blockInfo);

                }

            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditBlock(BlockInfo blockInfo)
        {
            
            if (string.IsNullOrEmpty(blockInfo.BlockImageName))
            {
                blockInfo.BlockImageName = "Def1";
            }

            Article article = new Article();
            if (blockInfo.ArticleId != 0)
            {
                article = await db.Articles.FirstOrDefaultAsync(p => p.Id == blockInfo.ArticleId);
            }
            else
            {
                article = DataClass.TempArticle;
            }



            if(db.Articles.FirstOrDefault(p => p.Name == blockInfo.ArticleName) != null)
            {
                blockInfo.ArticleId = article.Id;
                db.BlocksInfo.Update(blockInfo);
                await db.SaveChangesAsync();
                article.Blocks = db.BlocksInfo.Where(x => x.ArticleName == article.Name).ToList();
            }
            else
            {
                BlockInfo deleteBlockInfo = DataClass.TempList.FirstOrDefault(p => p.BlockName == blockInfo.PrevName);
                DataClass.TempList.Remove(deleteBlockInfo);
                DataClass.TempList.Add(blockInfo);
                article.Blocks = DataClass.TempList;
            }

            return View("Edit", article);
        }


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
