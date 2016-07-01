using System;

namespace Bitlink.Web.Models
{
    public class LinkViewModel
    {
        public long Id { get; set; }

        public string Url { get; set; }

        public string ShortUrl { get; set; }

        public string Hash { get; set; }

        public DateTime DateCreated { get; set; }

        public int ClickCount { get; set; }
    }
}