using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryPedia.Models;

namespace HistoryPedia.Models
{
    public class BlockInfo
    {
        public int BlockInfoId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public TableImage Image { get; set; }
        public int ArticleId { get; set; }
    }
}
