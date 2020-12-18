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
using HistoryPedia.Interfaces;
using HistoryPedia.signalR;
using HistoryPedia.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;


namespace HistoryPedia.Controllers
{
    public class HomeController : Controller
    {

        private ArticleContext db;
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext dbUsers;
        IHubContext<ChatHub> hubContext; 
        private readonly IGetArticle _getArticle;

        public HomeController(ILogger<HomeController> logger, ApplicationContext contextApp, ArticleContext contextArt, IHubContext<ChatHub> hubContext, IGetArticle getArticle)
        {
            db = contextArt;
            dbUsers = contextApp;
            _logger = logger;
            _getArticle = getArticle;
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

        public IActionResult Details(int id)
        {
            if (id != 0)
            {
                Article article = _getArticle.GetObjectArticle(id);
                article.Blocks = db.BlocksInfo.Where(x => x.ArticleId == article.Id).ToList();
                var Pictures = db.Pictures;
                article.Image = Pictures.FirstOrDefault(x => x.PictureName == article.ImageName);
                foreach (var item in article.Blocks)
                {
                    if (item.BlockImageName == null)
                        item.Image = Pictures.FirstOrDefault(x => x.PictureName == "Def1");
                    else
                        item.Image = Pictures.FirstOrDefault(x => x.PictureName == item.BlockImageName);
                }
                if (article != null)
                    return View(article);
            }

            return NotFound();
        }

        

        [Authorize(Roles = "admin")]
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            if (id != 0)
            {
                Article article = _getArticle.GetObjectArticle(id);
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
            var articles = string.IsNullOrEmpty(name)? _getArticle.GetAllArticles : db.Articles.Where(p => p.Name.Contains(name)).ToList();
            foreach (var item in articles)
            {
                item.Image = db.Pictures.FirstOrDefault(x => x.PictureName == item.ImageName);
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
                Article article = _getArticle.GetObjectArticle(id);
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
                article.Blocks = DataClass.BlocksTempList;
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
            article.Image = db.Pictures.FirstOrDefault(x => x.PictureName == article.ImageName);

            if (DataClass.BlocksTempList.Count != 0)
            {
                foreach (var item in DataClass.BlocksTempList)
                {
                    item.ArticleId = article.Id;
                    NewBlock(item);
                }
                DataClass.BlocksTempList.Clear();
            }

            if (DataClass.ImagesTempList.Count != 0)
            {
                foreach (var item in DataClass.ImagesTempList)
                {
                    Picture delPict = db.Pictures.FirstOrDefault(x => x.PictureName == item.PictureName);
                    if (delPict != null)
                    {
                        db.Pictures.Remove(delPict);

                    }
                    NewImage(item);
                }
                DataClass.ImagesTempList.Clear();
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

        [HttpPost]
        public void NewImage(Picture picture)
        {
            db.Pictures.Update(picture);
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
                var block = DataClass.BlocksTempList.FirstOrDefault(p => p.BlockName == name);
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
                var block = DataClass.BlocksTempList.FirstOrDefault(p => p.BlockName == name);
                DataClass.BlocksTempList.Remove(block);
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
                    DataClass.TempBlock = blockInfo;
                    return View(blockInfo);
                }
            }
            else
            {
                if (blockName != null)
                {
                    BlockInfo blockInfo = DataClass.BlocksTempList.FirstOrDefault(p => p.BlockName == blockName);
                    blockInfo.PrevName = blockInfo.BlockName;
                    DataClass.TempBlock = blockInfo;
                    return View(blockInfo);
                }
                else
                {
                    BlockInfo blockInfo = new BlockInfo();
                    blockInfo.ArticleName = DataClass.TempArticle.Name;
                    DataClass.TempBlock = blockInfo;
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
                //article = await db.Articles.FirstOrDefaultAsync(p => p.Id == blockInfo.ArticleId);
                article = _getArticle.GetObjectArticle(blockInfo.ArticleId);
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
                BlockInfo deleteBlockInfo = DataClass.BlocksTempList.FirstOrDefault(p => p.BlockName == blockInfo.PrevName);
                DataClass.BlocksTempList.Remove(deleteBlockInfo);
                DataClass.BlocksTempList.Add(blockInfo);
                article.Blocks = DataClass.BlocksTempList;
            }

            DataClass.TempBlock = null;
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

        public async Task<ActionResult> EditPicture(string pictureName, int type)
        {
            if (pictureName != null)
            {
                Picture picture = await db.Pictures.FirstOrDefaultAsync(p => p.PictureName == pictureName);
                if (picture == null)
                {
                    picture = DataClass.ImagesTempList.FirstOrDefault(p => p.PictureName == pictureName);
                }
                if (picture != null)
                {
                    PictureViewModel pictureLoad = new PictureViewModel();
                    pictureLoad.PictureText = picture.PictureText;
                    pictureLoad.PictureName = picture.PictureName;
                    pictureLoad.PicturePrevName = pictureLoad.PictureName;
                    pictureLoad.Type = type;
                    return View(pictureLoad);
                }
            }
            else
            {
                PictureViewModel picture = new PictureViewModel();
                picture.PictureName = "New picture";
                picture.Type = type;
                if (type == 1)
                {
                    DataClass.TempArticle.ImageName = picture.PictureName;
                }
                else
                {
                    DataClass.TempBlock.BlockImageName = picture.PictureName;
                }
                return View(picture);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> EditPicture(PictureViewModel pictureLoad)
        {
            Picture picture = new Picture();
            if (pictureLoad.PicturePrevName != null)
            {
                picture = db.Pictures.FirstOrDefault(x => x.PictureName == pictureLoad.PicturePrevName);
                if (picture == null)
                {
                    picture = DataClass.ImagesTempList.FirstOrDefault(p => p.PictureName == pictureLoad.PicturePrevName);
                }
            }

            if (pictureLoad.PictureName != null)
                picture.PictureName = pictureLoad.PictureName;
            if (pictureLoad.PictureText != null)
                picture.PictureText = pictureLoad.PictureText;
            if (pictureLoad.Image != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(pictureLoad.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int) pictureLoad.Image.Length);
                }

                // установка массива байтов
                picture.Image = imageData;
            }

            DataClass.ImagesTempList.Add(picture);


            if (pictureLoad.Type == 1)
            {
                DataClass.TempArticle.ImageName = pictureLoad.PictureName;
                return View("Edit", DataClass.TempArticle);
            }
            DataClass.TempBlock.BlockImageName = pictureLoad.PictureName;
            return View("EditBlock", DataClass.TempBlock);

        }
    }
}
