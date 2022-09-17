namespace CoffeeShopAPI.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string name) : base($"Bad request while trying to process method {name}!")
        {

        }
    }
}
