/*
Waseem Saidoun
11/29/2024

The objective of this lab is to apply your knowledge of different data types, conditional statements, loop
constructs, arrays, collections, methods, classes, inheritance, encapsulation, abstraction, and exception
handling in C# by developing a comprehensive grocery store inventory management system. This system
will allow users to manage a dynamic inventory of products with varying types, ensuring robust and
error-free operations through the use of object-oriented principles and exception handling.

*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment_6
{
    public abstract class Product // Abstract class Product to encapsulate the class properties
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public Product(string name, string category, int quantity, double price) // Constructor
        {
            Name = name;
            Category = category;
            Quantity = quantity;
            Price = price;
        }
        public abstract void Restock(int amount); 
        public abstract void Sell(int amount);
    }
    public class Perishable : Product // Perishable class inherits properties from Product
    {
        public Perishable(string name, string category, int quantity, double price, DateTime expirationDate) : base(name, category, quantity, price)
        {
            ExpirationDate = expirationDate;
        }
        public DateTime ExpirationDate { get; set; }
        public override void Sell(int amount) // Sell function
        {
            if (Quantity >= amount)
            {
                Quantity -= amount;
            }
            else if (Quantity < amount)
            {
                {
                    Console.WriteLine("You cannot sell more than what you have in stock");
                }
            }

        }
        public override void Restock(int amount) // Restock function
        {
            Quantity += amount;
        }
    }
    public class Beverage : Product // Beverage class inherits properties from Product
    {
        public Beverage(string name, string category, int quantity, double price, int volume) : base(name, category, quantity, price)
        {
            Volume = volume;
        }
        public int Volume { get; set; }
        public override void Restock(int amount)
        {
            Quantity += amount;
        }
        public override void Sell(int amount)
        {
            if (Quantity >= amount)
            {
                Quantity -= amount;
            }
            else if (Quantity < amount)
            {
                {
                    Console.WriteLine("You cannot sell more than what you have in stock");
                }
            }

        }
    }
    internal class Program
    {
        static void AddProduct(List<Product> products, string name, string category, int quantity, double price) // AddProduct function used to add new products to the inventory
        {
            Console.WriteLine("Are you adding a perishable item or beverage?");
            Console.WriteLine("1. Perishable");
            Console.WriteLine("2. Beverage");
            string choice = "";

            int volume;
            int day;
            int month;
            int year;
            DateTime expirationDate; // DateTime data type used to collect day month and year

            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter the day of expiration: ");
                    day = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the month of expiration: ");
                    month = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the year of expiration: ");
                    year = int.Parse(Console.ReadLine());

                    expirationDate = new DateTime(year, month, day);

                    Perishable perishable = new Perishable(name, category, quantity, price, expirationDate);
                    products.Add(perishable);
                    break;
                case "2":
                    Console.WriteLine("Enter the volume of your beverage in ml: ");
                    volume = int.Parse(Console.ReadLine());
                    Beverage beverage = new Beverage(name, category, quantity, price, volume);
                    products.Add(beverage);
                    break;
            }
            Console.WriteLine($"You have successfully added {name} to the inventory.");
        }
        static void RemoveProduct(List<Product> products, string name) // Function used to remove a product  from the inventory
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name == name)
                {
                    products.RemoveAt(i);
                }
            }
        }
        static void UpdateProductInfo(List<Product> products, string name, string newName, int newQuantity, double newPrice, string newCategory) // Function used to update a current products information
        {
            int day;
            int month;
            int year;
            DateTime expirationDate;
            int volume;
            bool foundProduct = false;

            foreach (Product product in products)
            {
                if (product.Name == name) // Converts each user input into a new name to update the information from the class list
                {
                    product.Name = newName;
                    product.Quantity = newQuantity;
                    product.Price = newPrice;
                    product.Category = newCategory;

                    if (product is Perishable perishable)
                    {
                        Console.WriteLine("What is the new expiration date of your perishable item?");
                        Console.WriteLine("Enter the day of expiration: ");
                        day = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the month of expiration: ");
                        month = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the year of expiration: ");
                        year = int.Parse(Console.ReadLine());
                        expirationDate = new DateTime(year, month, day);
                        perishable.ExpirationDate = expirationDate;

                        Console.WriteLine("Successfully updated your perishable item.");
                    }
                    else if (product is Beverage beverage)
                    {
                        Console.WriteLine("Enter the volume of your beverage: ");
                        volume = int.Parse(Console.ReadLine());
                        beverage.Volume = volume;

                        Console.WriteLine("Successfully updated your beverage.");
                    }
                    foundProduct = true;
                }
            }
            if (!foundProduct)
            {
                Console.WriteLine("This product does not exist.");
            }
        }
        static void SearchProduct(string nameOrCategory, List<Product> products) // Function used to search for a product in the inventory
        {
            bool foundProduct = false;

            foreach (Product product in products)
            {
                if (product.Name.Contains(nameOrCategory) || product.Category.Contains(nameOrCategory)) // Finds a product from user input that is either Category or Name
                {
                    Console.WriteLine($"Product name: {product.Name}");
                    Console.WriteLine($"Category: {product.Category}");
                    Console.WriteLine($"Quantity: {product.Quantity}");
                    Console.WriteLine($"Price: {product.Price}");

                    if (product is Beverage beverage)
                    {
                        Console.WriteLine($"Beverage volume: {beverage.Volume}ml");
                    }
                    else if (product is Perishable perishable)
                    {
                        Console.WriteLine($"Expiration date: {perishable.ExpirationDate}");
                    }
                    foundProduct = true;
                }
                Console.WriteLine(); // If it cannot find product, error message
                if (!foundProduct)
                {
                    Console.WriteLine("This item does not exist in the inventory.");
                }
            }
        }
        static void RestockProduct(string name, List<Product> products) // Function used to add more quantity to the stock of an existing item
        {
            int amount;
            foreach (Product product in products)
            {
                if (product.Name.Contains(name))
                {
                    Console.WriteLine("How many more of this product would you like to restock into the inventory?");
                    amount = int.Parse(Console.ReadLine());
                    product.Restock(amount);
                }
            }

        }
        static void SellProduct(string name, List<Product> products) // Function used to remove stock from an existing item
        {
            int amount;
            foreach (Product product in products)
            {
                if (product.Name.Contains(name))
                {
                    Console.WriteLine("How much are you selling of this product?");
                    amount = int.Parse(Console.ReadLine());
                    product.Sell(amount);
                }
            }
        }
        static void SortProduct(List<Product> products) // Extra: This sorts the products by name category or price
        {
            Console.WriteLine("How would you like your products to be sorted by?");
            Console.WriteLine("1. Name.");
            Console.WriteLine("2. Category.");
            Console.WriteLine("3. Price.");
            string choice = "";
            switch (choice)
            {
                case "1":
                    products = products.OrderBy(product => product.Name).ToList();
                    break;
                case "2":
                    products = products.OrderBy(product => product.Category).ToList();
                    break;
                case "3":
                    products = products.OrderBy(product => product.Price).ToList();
                    break;
            }
        }
        static void SaveList(List<Product> products) // Extra: Saves the entire list of items in our inventory to a text file
        {
            string docPath =
          Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Inventory.txt")))
            {
                foreach (Product product in products)
                {
                    outputFile.WriteLine(product.Name);
                    outputFile.WriteLine(product.Category);
                    outputFile.WriteLine(product.Quantity);
                    outputFile.WriteLine(product.Price);
                    if (product is Beverage beverage)
                    {
                        outputFile.WriteLine(beverage.Volume);
                    }
                    else if (product is Perishable perishable)
                    {
                        outputFile.WriteLine(perishable.ExpirationDate);
                    }
                }
            }
        }
        static List<Product> Load() // Extra: Function that loads right when the program starts, reads from the text file created to keep track of the inventory
        {
            List<Product> products = new List<Product>();
            string docPath =
          Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!File.Exists(Path.Combine(docPath, "Inventory.txt")))
            {
                return products;
            }
            StreamReader sr = new StreamReader(Path.Combine(docPath, "Inventory.txt"));

            string line = sr.ReadLine();
            string name;
            string category;
            int quantity;
            double price;
            while (line != null)
            {
                name = line;
                category = sr.ReadLine();
                quantity = Int32.Parse(sr.ReadLine());
                price = Double.Parse(sr.ReadLine());

                try 
                {
                    line = sr.ReadLine();
                    int volume = Int32.Parse(line);
                    Beverage beverage = new Beverage(name, category, quantity, price, volume);
                    products.Add(beverage);
                }
                catch (Exception ex)
                {
                    List<string> expirationDateArray = line.Split('-').ToList();
                    DateTime expirationDate = new DateTime(Int32.Parse(expirationDateArray[0]), Int32.Parse(expirationDateArray[1]), Int32.Parse(expirationDateArray[2].Substring(0, 2)));
                    Perishable perishable = new Perishable(name, category, quantity, price, expirationDate);
                    products.Add(perishable);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            return products;
        }
        static void Main(string[] args)
        {
            var products = Load(); // Loads the inventory from the text file

            string productName = "Milk";
            string productCategory = "Dairy";
            int productQuantity = 50;
            double productPrice = 2.99;
            string choice = "";

            while (choice != "10") // User interface showing every option
            {
                Console.WriteLine("Welcome to the grocery store inventory system, here are a list of options to choose from: ");
                Console.WriteLine("1. View all products in our inventory.");
                Console.WriteLine("2. Add a new product to the inventory.");
                Console.WriteLine("3. Remove a product from the inventory.");
                Console.WriteLine("4. Update the information of an existing product.");
                Console.WriteLine("5. Search for a product in the inventory.");
                Console.WriteLine("6. Restock a product in the inventory.");
                Console.WriteLine("7. Sell a product from the inventory.");
                Console.WriteLine("8. Sort products.");
                Console.WriteLine("9. Save list.");
                Console.WriteLine("10. Exit.");
                choice = Console.ReadLine();

                switch (choice) // Switch cases to take user input and output their decisions
                {
                    case "1":
                        Console.WriteLine("Here is a list of every product in our inventory: ");
                        for (int i = 0; i < products.Count; ++i)
                        {
                            Console.WriteLine(products[i].Name);
                        }
                        break;
                    case "2":
                        Console.WriteLine("What is the product name you'd like to add?");
                        productName = Console.ReadLine();
                        Console.WriteLine("What is the category of the new product?");
                        productCategory = Console.ReadLine();
                        Console.WriteLine("What is the quantity of the new product?");
                        productQuantity = int.Parse(Console.ReadLine());
                        Console.WriteLine("What is the price of the new product?");
                        productPrice = double.Parse(Console.ReadLine());

                        AddProduct(products, productName, productCategory, productQuantity, productPrice);
                        break;
                    case "3":
                        Console.WriteLine("What is the product name you would like to remove?");
                        productName = Console.ReadLine();
                        RemoveProduct(products, productName);
                        break;
                    case "4":
                        Console.WriteLine("What is the name of the product you would like to update?");
                        productName = Console.ReadLine();
                        Console.WriteLine("What is the new product name?");
                        string newName = Console.ReadLine();
                        Console.WriteLine("What is the new category?");
                        productCategory = Console.ReadLine();
                        Console.WriteLine("What is the new quantity?");
                        productQuantity = int.Parse(Console.ReadLine());
                        Console.WriteLine("What is the new price?");
                        productPrice = double.Parse(Console.ReadLine());
                        UpdateProductInfo(products, productName, newName, productQuantity, productPrice, productCategory);
                        break;
                    case "5":
                        Console.WriteLine("What is the name of the product or the category?");
                        string nameOrCategory = Console.ReadLine();
                        SearchProduct(nameOrCategory, products);
                        break;
                    case "6":
                        Console.WriteLine("Enter the name of the product you would like to restock: ");
                        productName = Console.ReadLine();
                        RestockProduct(productName, products);
                        break;
                    case "7":
                        Console.WriteLine("Enter the name of the product you would like to sell: ");
                        productName = Console.ReadLine();
                        SellProduct(productName, products);
                        break;
                    case "8":
                        SortProduct(products);
                        break;
                    case "9":
                        SaveList(products);
                        break;
                    case "10":
                        break;
                }
            }
        }
    }
}
