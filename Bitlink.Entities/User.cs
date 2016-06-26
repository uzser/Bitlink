using Bitlink.Entities.Common;
using System;
using System.Collections.Generic;

namespace Bitlink.Entities
{
    public class User : IEntityBase
    {
        public User()
        {
            Links = new List<Link>();
        }

        public long Id { get; set; }

        public Guid Uid { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual ICollection<Link> Links { get; private set; }
    }
}