using System;

public class ComputerShop
{
    private static ComputerShop _instance;
    private static readonly object _lock = new object(); 

    private ComputerShop() {
        Console.WriteLine("Welcome to computer shop");
    }

    public static ComputerShop GetInstance()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = new ComputerShop();
            }
            return _instance;
        }
    }
}
