namespace Shops.Models;

public class ShopAddress
{
    public ShopAddress(string address)
    {
        if (address is null) throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentNullException();
        Address = address;
    }

    public string Address { get; }
}