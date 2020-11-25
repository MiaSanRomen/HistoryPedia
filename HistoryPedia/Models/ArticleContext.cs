using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryPedia.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryPedia.Models
{
    public class ArticleContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Change> Changes { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<BlockInfo> BlocksInfo { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public ArticleContext(DbContextOptions<ArticleContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
