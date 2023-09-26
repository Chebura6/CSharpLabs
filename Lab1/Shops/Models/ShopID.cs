namespace Shops.Models;

public class ShopID
{
    public ShopID()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}
