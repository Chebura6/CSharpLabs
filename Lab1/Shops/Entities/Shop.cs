using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private static uint _shopsCounter;
    private List<ShopProductElement> _productsList;
    public Shop(ShopName name, ShopAddress address)
    {
        ShopName = name ?? throw new ArgumentNullException();
        Address = address ?? throw new ArgumentNullException();
        _shopsCounter++;
        _productsList = new List<ShopProductElement>();
        ShopId = new ShopID();
    }

    public ShopName ShopName { get; }
    public ShopID ShopId { get; }
    public ShopAddress Address { get; }
    public IReadOnlyCollection<ShopProductElement> Products => _productsList;

    public void AddNewProduct(Product product, uint count)
    {
        ArgumentNullException.ThrowIfNull(product, "Null product detected");
        ShopProductElement isExistProduct = _productsList.FirstOrDefault(oldProduct => oldProduct.Product == product);
        if (isExistProduct is not null)
        {
            isExistProduct.ProductsCount += count;
            isExistProduct.Product.ChangeProductPrice(product.Price);
        }
        else
        {
            var shopProductElement = new ShopProductElement((Product)product.Clone(), count);
            _productsList.Add(shopProductElement);
        }
    }

    public void AddNewProducts(List<Product> newProducts, List<uint> productsCount)
    {
        ArgumentNullException.ThrowIfNull(newProducts, "Null products list detected");
        ArgumentNullException.ThrowIfNull(productsCount, "Null products count list detected");
        if (newProducts.Count != productsCount.Count) throw ShopException.DifferentListsCount();
        for (int i = 0; i < newProducts.Count; ++i)
        {
            AddNewProduct(newProducts[i], productsCount[i]);
        }
    }

    public void ChangePrice(Product product, decimal newPrice)
    {
        ArgumentNullException.ThrowIfNull(product, "Null product detected");
        ShopProductElement productWithIrrelevantPrice = FindProduct(product);
        if (productWithIrrelevantPrice is null)
        {
            throw ShopException.ProductNotFound();
        }

        productWithIrrelevantPrice.Product.ChangeProductPrice(newPrice);
    }

    public override string ToString()
    {
        return ShopName + " " + Address;
    }

    internal decimal? ProductsCheck(ShoppingListElement product)
    {
        ArgumentNullException.ThrowIfNull(product, "Null shopping list element detected");
        ShopProductElement purchase = FindProduct(product.Product);
        if (purchase is null)
        {
            throw ShopException.ProductNotFound();
        }

        if (purchase.ProductsCount >= product.ProductsCount)
        {
            return product.ProductsCount * purchase.Product.Price;
        }

        return null;
    }

    internal void Buy(ShoppingListElement product)
    {
        ArgumentNullException.ThrowIfNull(product, "Null shopping list element detected");
        ShopProductElement purchase = FindProduct(product.Product);
        purchase.ProductsCount -= product.ProductsCount;
    }

    private ShopProductElement FindProduct(Product product)
    {
        ShopProductElement shopProduct = _productsList.FirstOrDefault(p => p.Product.ProductName == product.ProductName);
        if (shopProduct is null) return null;
        return shopProduct;
    }
}