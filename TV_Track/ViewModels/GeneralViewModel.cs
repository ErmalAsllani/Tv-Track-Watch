using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_Track.Models;

namespace TV_Track.ViewModels
{
    public class GeneralViewModel
    {
        public string SearchTerm { get; set; }
        public List<GeneralMedia> Media { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages  { get; set; }
        public int TotalResults { get; set; }
        public int GenreID { get; set; }
    }

}
