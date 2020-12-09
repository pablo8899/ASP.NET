namespace Wsei.ExchangeThings.Web.Controllers
{
    public class AddNewItemResponseModel<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}