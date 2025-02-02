using System;

public interface IProductState
{
    void Handle(ProductContext context);
}

public class InStockState : IProductState
{
    public void Handle(ProductContext context)
    {
        Console.WriteLine("Product is In Stock.");
        context.SetState(this);
    }
}

public class OutOfStockState : IProductState
{
    public void Handle(ProductContext context)
    {
        Console.WriteLine("Product is Out of Stock.");
        context.SetState(this);
    }
}

public class ProductContext
{
    private IProductState _state;

    public ProductContext()
    {
        _state = new InStockState(); 
    }

    public void SetState(IProductState state)
    {
        _state = state;
    }

   
    public string GetState()
    {
        return _state.GetType().Name; 
    }

 
}

