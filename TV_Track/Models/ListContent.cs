using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class ListContent
    {
        public int ListContentId { get; set; }
        public int ListId { get; set; }
        public int? MovieId { get; set; }
        public int? TvSeriesId { get; set; }

        public virtual List List { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual TvSeries TvSeries { get; set; }
    }
}
