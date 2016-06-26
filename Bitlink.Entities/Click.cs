using Bitlink.Entities.Common;
using System;

namespace Bitlink.Entities
{
    public class Click : IEntityBase
    {
        public long Id { get; set; }

        public long LinkId { get; set; }

        public DateTime Date { get; set; }

        public virtual Link Link { get; set; }
    }
}
