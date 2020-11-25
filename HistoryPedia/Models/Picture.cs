using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryPedia.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
    }
}
