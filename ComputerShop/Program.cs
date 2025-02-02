using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        
        ManagerProxy proxy = new ManagerProxy();
        int loginAttempts = 0;
        bool authenticated = false;

        while (loginAttempts < 3 && !authenticated)
        {
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty.");
                continue;
            }

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password cannot be empty.");
                continue;
            }

            try
            {
                if (proxy.Authenticate(email, password))
                {
                    Console.WriteLine("Authentication successful!");
                    authenticated = true;
                }
                else
                {
                    throw new UnauthorizedAccessException("Invalid credentials.");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
                loginAttempts++;
                if (loginAttempts == 3)
                {
                    Console.WriteLine("Too many failed attempts. Exiting...");
                    return;
                }
            }
        }

        ProductCollection productCollection = new ProductCollection();
        ConcreteProductFactory factory = new ConcreteProductFactory();

        
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Add Products");
            Console.WriteLine("2. View Products");
            Console.WriteLine("3. Change Product State");
            Console.WriteLine("4. Sort Products");
            Console.WriteLine("5. Exit");

            int choice;
            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter product type (1 for Laptop, 2 for Accessory):");
                    int productChoice;
                    try
                    {
                        productChoice = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid product type. Please enter a number (1 or 2).");
                        continue;
                    }

                    if (productChoice < 1 || productChoice > 2)
                    {
                        Console.WriteLine("Invalid product type.");
                        continue;
                    }

                    Console.WriteLine("Enter product name:");
                    string productName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(productName))
                    {
                        Console.WriteLine("Product name cannot be empty.");
                        continue;
                    }

                    Console.WriteLine("Enter product price:");
                    double productPrice;
                    while (!double.TryParse(Console.ReadLine(), out productPrice) || productPrice <= 0)
                    {
                        Console.WriteLine("Invalid price. Please enter a positive number:");
                    }

                    if (productChoice == 1)
                    {
                        productCollection.AddProduct(factory.CreateProduct("Laptop", productName, productPrice));
                    }
                    else if (productChoice == 2)
                    {
                        productCollection.AddProduct(factory.CreateProduct("Accessory", productName, productPrice));
                    }
                    break;

                case 2:
                    Console.WriteLine("Choose product type to view:");
                    Console.WriteLine("1. Laptops");
                    Console.WriteLine("2. Accessories");

                    int productChoicee;
                    try
                    {
                        productChoicee = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        continue;
                    }

                    string productType = productChoicee == 1 ? "Laptop" : productChoicee == 2 ? "Accessory" : null;

                    if (productType == null)
                    {
                        Console.WriteLine("Invalid choice.");
                        continue;
                    }

                    IProductIterator iterator = productCollection.CreateIterator(productType);

                    Console.WriteLine($"\n{productType}s:");
                    int index = 1;
                    if (!iterator.HasNext())
                    {
                        Console.WriteLine($"No {productType}s available.");
                    }
                    else
                    {
                        while (iterator.HasNext())
                        {
                            var product = iterator.Next();
                            Console.WriteLine($"{index}. {product.GetProductDetails()}");
                            index++;
                        }
                    }
                    break;

                case 3:
                    if (productCollection._products.Count == 0)
                    {
                        Console.WriteLine("No products available to change state.");
                        break;
                    }

                    Console.WriteLine("Displaying all products:");
                    int idx = 1;
                    foreach (var product in productCollection._products)
                    {
                        Console.WriteLine($"{idx}. {product.GetProductDetails()}");
                        idx++;
                    }

                    Console.WriteLine("\nEnter the product number to change state:");
                    int productIndex;
                    try
                    {
                        productIndex = int.Parse(Console.ReadLine()) - 1;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid product number.");
                        continue;
                    }

                    if (productIndex >= 0 && productIndex < productCollection._products.Count)
                    {
                        var product = productCollection._products[productIndex];
                        Console.WriteLine("Enter new state (1 for InStock, 2 for OutOfStock):");

                        int stateChoice;
                        try
                        {
                            stateChoice = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid state choice. Please enter 1 or 2.");
                            continue;
                        }

                        if (stateChoice == 1)
                        {
                            product.ProductContext.SetState(new InStockState());
                            Console.WriteLine("Product state set to InStock.");
                        }
                        else if (stateChoice == 2)
                        {
                            product.ProductContext.SetState(new OutOfStockState());
                            Console.WriteLine("Product state set to OutOfStock.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid product selection.");
                    }
                    break;

                case 4:
                    if (productCollection._products.Count == 0)
                    {
                        Console.WriteLine("No products available to sort.");
                        break;
                    }

                    Console.WriteLine("Sort products by:");
                    Console.WriteLine("1. Name");
                    Console.WriteLine("2. Price");

                    int sortChoice;
                    try
                    {
                        sortChoice = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        continue;
                    }

                    var sortContext = sortChoice == 1 ? new ProductSortContext(new SortByName()) : new ProductSortContext(new SortByPrice());
                    var sortedProducts = sortContext.ExecuteSort(productCollection._products);
                    foreach (var product in sortedProducts)
                    {
                        Console.WriteLine(product.GetProductDetails());
                    }
                    break;

                case 5:
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}