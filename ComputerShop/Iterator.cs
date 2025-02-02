using System;
using System.Collections.Generic;
using System.Linq;

public interface IProductCollection
{
    IProductIterator CreateIterator(string productType);
}

public interface IProductIterator
{
    bool HasNext();

    IProduct Next();
}

public class LaptopIterator : IProductIterator
{
    private List<IProduct> _products;
    private int _currentIndex = 0;

    public LaptopIterator(List<IProduct> products)
    {
        _products = products.Where(p => p is Laptop).ToList();  
    }

    public bool HasNext() => _currentIndex < _products.Count;

    public IProduct Next() => _products[_currentIndex++];
}

public class AccessoryIterator : IProductIterator
{
    private Stack<IProduct> _products;

    public AccessoryIterator(List<IProduct> products)
    {
        
        _products = new Stack<IProduct>(products.Where(p => p is Accessory));
    }

    public bool HasNext() => _products.Count > 0;

    public IProduct Next()
    {
        if (!HasNext())
        {
            throw new InvalidOperationException("No more products.");
        }
        return _products.Pop(); 
    }
}


public class ProductCollection : IProductCollection
{
    public List<IProduct> _products = new List<IProduct>();

    public void AddProduct(IProduct product)
    {
        _products.Add(product);
    }

  
    public IProductIterator CreateIterator(string productType)
    {
        if (productType == "Laptop")
        {
            return new LaptopIterator(_products);
        }
        else if (productType == "Accessory")
        {
            return new AccessoryIterator(_products);
        }
        else
        {
            throw new ArgumentException("Invalid product type");
        }
    }
}

