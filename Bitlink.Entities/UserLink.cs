using Bitlink.Entities.Common;

namespace Bitlink.Entities
{
    public class UserLink : IEntityBase
    {
        public long Id { get; set; }

        public long LinkId { get; set; }

        public long UserId { get; set; }
    }
}
