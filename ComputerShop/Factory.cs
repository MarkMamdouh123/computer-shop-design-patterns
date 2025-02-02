using System;


public interface IProduct
{
    string Name { get; set; }
    double Price { get; set; }
    string GetProductDetails();
    ProductContext ProductContext { get; set; }  
}

public class Laptop : IProduct
{
    public string Name { get; set; }
    public double Price { get; set; }
    public ProductContext ProductContext { get; set; }

    public Laptop(string name, double price)
    {
        Name = name;
        Price = price;
        ProductContext = new ProductContext();  
    }

    public string GetProductDetails()
    {
        return $"Product: {Name}, Price: ${Price}, Status: {ProductContext.GetState()}";
    }
}

public class Accessory : IProduct
{
    public string Name { get; set; }
    public double Price { get; set; }
    public ProductContext ProductContext { get; set; }

    public Accessory(string name, double price)
    {
        Name = name;
        Price = price;
        ProductContext = new ProductContext();  
    }

    public string GetProductDetails()
    {
        return $"Product: {Name}, Price: ${Price}, Status: {ProductContext.GetState()}";
    }
}

public abstract class ProductFactory
{
    public abstract IProduct CreateProduct(string productType, string name, double price);
}

public class ConcreteProductFactory : ProductFactory
{
    public override IProduct CreateProduct(string productType, string name, double price)
    {
        if (productType == "Laptop")
        {
            return new Laptop(name, price);
        }
        else if (productType == "Accessory")
        {
            return new Accessory(name, price);
        }
        else
        {
            throw new ArgumentException("Invalid product type");
        }
    }
}


