using Shops.Exceptions;

namespace Shops.Models;

public class Product : ICloneable, IEquatable<Product>
{
    public Product(string productName, decimal price)
    {
        ProductName = productName ?? throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException();
        if (price < 0) throw ProductException.NegativePrice();
        Price = price;
        Id = Guid.NewGuid();
    }

    public string ProductName { get; }
    public decimal Price { get; private set; }
    internal Guid Id { get; }

    public void ChangeProductPrice(decimal newPrice)
    {
        if (newPrice < 0) throw ProductException.NegativePrice();
        Price = newPrice;
    }

    public bool Equals(Product other)
    {
        if (other is null) return false;
        return Id == other.Id;
    }

    public bool Eqauals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Product)obj);
    }

    public override int GetHashCode()
    {
        return ProductName.GetHashCode();
    }

    public object Clone()
    {
        return (Product)MemberwiseClone();
    }
}