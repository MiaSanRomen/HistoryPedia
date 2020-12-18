using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryPedia.Models;

namespace HistoryPedia.Interfaces
{
    public interface IGetFavorite
    {
        List<Article> GetFavorite(string id);
    }
}
