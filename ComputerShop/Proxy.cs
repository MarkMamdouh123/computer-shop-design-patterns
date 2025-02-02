using System;

public interface IUserAuthentication
{
    bool Authenticate(string userName, string password);
}

public class ManagerProxy : IUserAuthentication
{
    private readonly Manager _manager;

    public ManagerProxy()
    {
        _manager = new Manager();

      
    }

    public bool Authenticate(string userName, string password)
    {
        bool isAuth = false;
        
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("username and password cannot be empty.");
            throw new UnauthorizedAccessException("Authentication failed due to empty credentials.");
        }

        if (userName == "mark" && password == "123") 
        {
            
           isAuth= _manager.Authenticate(userName, password);

        }
        else
        {
            Console.WriteLine("Invalid credentials.");
            throw new UnauthorizedAccessException("Invalid username or password.");
        }
        return isAuth;
    }
}

public class Manager : IUserAuthentication
{
    public bool Authenticate(string userName, string password)
    {
        
        Console.WriteLine($"{userName}  have logged in successfully.");
        return true;
    }
}
