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
        public string BlockName { get; set; }
        public int PrevId { get; set; }
        public string Text { get; set; }
        public string BlockImageName { get; set; }
        public Picture Image { get; set; }
        public int ArticleId { get; set; }
    }
}
