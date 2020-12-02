using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryPedia.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShirtInfo { get; set; }
        public string Info { get; set; }
        public string ImageName { get; set; }
        public List<BlockInfo> Blocks { get; set; }
        public Picture Image { get; set; }
        public string UserId { get; set; }
    }

}