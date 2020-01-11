using Rtl.TVMaze.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rtl.TVMaze.Service.Services
{
    public interface IMetaDataScraper
    {
        IEnumerable<Show> GetShows(int page);

        void EnrichShows(IEnumerable<Show> shows);
    }
}
