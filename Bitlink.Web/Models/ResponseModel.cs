namespace Bitlink.Web.Models
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }

        public string StatusMessage { get; set; }
    }
}