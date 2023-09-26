namespace Shops.Models;

public class ShopProductElement
{
    public ShopProductElement(Product product, uint productsCount)
    {
        Product = product ?? throw new ArgumentNullException();
        ProductsCount = productsCount;
    }

    public Product Product { get; }
    public uint ProductsCount { get; set; }
    public bool Equals(ShopProductElement other)
    {
        if (other is null) return false;
        return Product.ProductName == other.Product.ProductName;
    }

    public bool Eqauals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ShopProductElement)obj);
    }
}