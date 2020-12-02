using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryPedia.Models
{
    public class SearchArticle
    {
        public User User { get; set; }
        public List<Article> Articles { get; set; }
    }
}
