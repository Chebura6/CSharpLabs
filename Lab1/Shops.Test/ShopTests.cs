using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopTests
{
    private ShopName _lenta;
    private ShopAddress _address;
    private ShopManager _shopManager;
    public ShopTests()
    {
        _lenta = new ShopName("Lenta");
        _address = new ShopAddress("Leninsky 2");
        _shopManager = new ShopManager();
    }

    [Fact]
    public void SetPrice()
    {
        var client = new Client(1000);
        var shop = new Shop(_lenta, _address);
        var product1 = new Product("Milk", 100);
        var product2 = new Product("Chocolate", 200);
        var products = new List<Product> { product1, product2 };
        _shopManager.AddShopInSystem(shop);
        shop.AddNewProducts(products, new List<uint> { 5, 5 });
        shop.ChangePrice(product1, 300);
        shop.ChangePrice(product2, 500);
        product1.ChangeProductPrice(300);
        product2.ChangeProductPrice(500);
        Assert.Equal(product1.Price, shop.Products.ElementAt(0).Product.Price);
        Assert.Equal(product2.Price, shop.Products.ElementAt(1).Product.Price);
    }

    [Fact]
    public void CreateSom3ePurchase()
    {
        var client = new Client(1000);
        var shop = new Shop(_lenta, _address);
        var shoppingList = new List<ShoppingListElement>();
        var product1 = new Product("Milk", 100);
        var product2 = new Product("Chocolate", 200);
        var products = new List<Product> { product1, product2 };
        _shopManager.AddShopInSystem(shop);
        shop.AddNewProduct(product1, 5);
        shop.AddNewProduct(product2, 5);
        shoppingList.Add(new ShoppingListElement(product1, 1));
        shoppingList.Add(new ShoppingListElement(product2, 2));
        _shopManager.CreateSomePurchases(client, shop, shoppingList);
        Assert.Equal(4U, shop.Products.ElementAt(0).ProductsCount);
        Assert.Equal(3U, shop.Products.ElementAt(1).ProductsCount);
        Assert.Equal(500, client.Balance);
    }

    [Fact]
    public void CreateSomePurchaseError()
    {
        var client = new Client(1000);
        var shop = new Shop(_lenta, _address);
        var shoppingList = new List<ShoppingListElement>();
        var product1 = new Product("Milk", 100);
        var product2 = new Product("Chocolate", 200);
        var products = new List<Product> { product1, product2 };
        _shopManager.AddShopInSystem(shop);
        shop.AddNewProduct(product1, 5);
        shop.AddNewProduct(product2, 5);
        shoppingList.Add(new ShoppingListElement(product1, 6));
        shoppingList.Add(new ShoppingListElement(product2, 2));
        Assert.Throws<ShopManagerException>(() =>
            _shopManager.CreateSomePurchases(client, shop, shoppingList));
    }

    [Fact]
    public void CheapestPurchase()
    {
        var client = new Client(1000);
        var magnit = new ShopName("Magnit");
        var ozon = new ShopName("Ozon");
        var address1 = new ShopAddress("Lenina 1");
        var address2 = new ShopAddress("Kosmonavtov 37");
        var shop = new Shop(magnit, _address);
        var shop1 = new Shop(ozon, address1);
        var shop2 = new Shop(_lenta, address2);
        var product1 = new Product("Milk", 100);
        var product2 = new Product("Milk", 50);
        var product3 = new Product("Milk", 200);
        shop.AddNewProduct(product1, 5);
        shop1.AddNewProduct(product2, 5);
        shop2.AddNewProduct(product3, 5);
        _shopManager.AddShopInSystem(shop);
        _shopManager.AddShopInSystem(shop1);
        _shopManager.AddShopInSystem(shop2);
        var shoppingList = new List<ShoppingListElement> { new ShoppingListElement(product1, 2) };
        Assert.Equal(_shopManager.FindCheapestShop(client, shoppingList), shop1);
    }
}