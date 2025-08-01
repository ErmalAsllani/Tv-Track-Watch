using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class List
    {
        public List()
        {
            ListContents = new HashSet<ListContent>();
        }

        public int ListId { get; set; }
        public string ListName { get; set; }
        public string UserId { get; set; }
        public int ListTypeId { get; set; }

        public virtual ListType ListType { get; set; }
        public virtual AspNetUser User { get; set; }
        public virtual ICollection<ListContent> ListContents { get; set; }
    }
}
