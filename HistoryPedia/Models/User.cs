using System.Collections.Generic;
using HistoryPedia.Models;
using Microsoft.AspNetCore.Identity;

namespace HistoryPedia.Models
{
    public class User : IdentityUser
    {
        public List<Article> FavoriteArticles { get; set; }
    }
}