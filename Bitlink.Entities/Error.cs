using Bitlink.Entities.Common;
using System;

namespace Bitlink.Entities
{
    public class Error : IEntityBase
    {
        public long Id { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
