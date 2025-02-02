using System.Collections.Generic;
using System.Linq;

public interface IProductSortStrategy
{
    List<IProduct> Sort(List<IProduct> products);
}

public class SortByName : IProductSortStrategy
{
    public List<IProduct> Sort(List<IProduct> products)
    {
        return products.OrderBy(p => p.Name).ToList();
    }
}

public class SortByPrice : IProductSortStrategy
{
    public List<IProduct> Sort(List<IProduct> products)
    {
        return products.OrderBy(p => p.Price).ToList();
    }
}

public class ProductSortContext
{
    private IProductSortStrategy _sortStrategy;

    public ProductSortContext(IProductSortStrategy sortStrategy)
    {
        _sortStrategy = sortStrategy;
    }

    public List<IProduct> ExecuteSort(List<IProduct> products)
    {
        return _sortStrategy.Sort(products);
    }
}
