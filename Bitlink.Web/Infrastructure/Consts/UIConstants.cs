namespace Bitlink.Web.Infrastructure.Consts
{
    public static class UIConstants
    {
        public static int LinkHashLength = 6;

        public static class StatusMessage
        {
            public static string Ok = "OK";
            public static string InvalidUrl = "INVALID_URL";
            public static string LinkExists = "LINK_EXISTS";
            public static string AlreadyShortenedLink = "ALREADY_SHORTENED_LINK";
        }
    }
}