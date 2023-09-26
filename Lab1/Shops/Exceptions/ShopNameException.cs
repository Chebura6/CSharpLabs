namespace Shops.Exceptions;

public class ShopNameException : Exception
{
    private ShopNameException(string message)
        : base(message) { }
    public static ShopNameException InvalidShopName()
    {
        return new ShopNameException("Invalid shop name");
    }
}