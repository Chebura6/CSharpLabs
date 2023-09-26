namespace Shops.Models;

public class ShoppingListElement
{
    public ShoppingListElement(Product product, uint productsCount)
    {
        if (product is null) throw new ArgumentNullException("Null product");
        Product = product;
        ProductsCount = productsCount;
    }

    public Product Product { get; set; }
    public uint ProductsCount { get; set; }
}