using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class ListType
    {
        public ListType()
        {
            Lists = new HashSet<List>();
        }

        public int ListTypeId { get; set; }
        public string ListTypeName { get; set; }

        public virtual ICollection<List> Lists { get; set; }
    }
}
