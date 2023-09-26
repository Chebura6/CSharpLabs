namespace Shops.Exceptions;

public class ProductException : Exception
{
    private ProductException(string message)
        : base(message) { }

    public static ProductException NegativePrice()
    {
        return new ProductException("Product cannot has negative price");
    }
}