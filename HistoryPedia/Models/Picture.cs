﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HistoryPedia.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string PictureName { get; set; }
        public string Path { get; set; }
        public string PictureText { get; set; }
        public byte[] Image { get; set; }
    }
}
