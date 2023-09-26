using Shops.Exceptions;

namespace Shops.Models;

public class ShopName
{
    public ShopName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) && string.IsNullOrEmpty(name)) throw new ArgumentNullException();
        Name = name;
    }

    public string Name { get; }
}