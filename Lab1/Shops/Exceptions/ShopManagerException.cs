namespace Shops.Exceptions;

public class ShopManagerException : Exception
{
    private ShopManagerException(string message)
        : base(message) { }

    public static ShopManagerException TransactionError()
    {
        return new ShopManagerException("Transaction wasn't successful");
    }

    public static ShopManagerException SomePurchaseMakingError()
    {
        return new ShopManagerException("Unsuccessful purchase");
    }

    public static ShopManagerException NotEnoughProductsInShop()
    {
        return new ShopManagerException("Not enough products");
    }
}