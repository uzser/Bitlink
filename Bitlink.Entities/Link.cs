using Bitlink.Entities.Common;
using System;
using System.Collections.Generic;

namespace Bitlink.Entities
{
    public class Link : IEntityBase
    {
        public Link()
        {
            Users = new List<User>();
            Clicks = new List<Click>();
        }

        public long Id { get; set; }

        public string Url { get; set; }

        public string Hash { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual ICollection<User> Users { get; private set; }

        public virtual ICollection<Click> Clicks { get; private set; }
    }
}
