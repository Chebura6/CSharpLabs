namespace Shops.Exceptions;

public class ShopException : Exception
{
    private ShopException(string message)
        : base(message) { }
    public static ShopException ProductNotFound()
    {
        return new ShopException("Product hasn't found");
    }

    public static ShopException NegativePrice()
    {
        return new ShopException("Negative price is impossible");
    }

    public static ShopException InvalidProductName()
    {
        return new ShopException("Invalid product name");
    }

    public static ShopException DifferentListsCount()
    {
        return new ShopException("List of products and list of product's count have different lenght");
    }
}