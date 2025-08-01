using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_Track.ViewModels
{
    public class GeneralMedia
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string MediaType { get; set; }
        public string VoteAverage { get; set; }
        public List<string> GenreIdList { get; set; }
    }
}
