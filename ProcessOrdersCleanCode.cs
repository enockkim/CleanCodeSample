using System;
using System.Collections.Generic;
using System.IO;

namespace CleanCodeSample;
public class ProcessOrdersCleanCode
{
    public void ProcessOrders()
    {
        string filePath = "./Data/orders.csv";

        var lines = LoadLines(filePath);
        var orders = ParseOrders(lines);
        var total = CalculateTotal(orders);

        PrintOrders(orders);
        PrintTotal(total);
    }

    // Procedures (reusable methods) 

    static IEnumerable<string> LoadLines(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("Missing orders file.", path);

        return File.ReadAllLines(path);
    }

    static List<Order> ParseOrders(IEnumerable<string> lines)
    {
        var orders = new List<Order>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(',');
            if (parts.Length != 2) continue;

            if (!double.TryParse(parts[1], out double price)) continue;

            orders.Add(new Order(parts[0], price));
        }

        return orders;
    }

    static double CalculateTotal(IEnumerable<Order> orders)
    {
        double total = 0;

        foreach (var order in orders)
            total += order.Price;

        return total;
    }

    static void PrintOrders(IEnumerable<Order> orders)
    {
        Console.WriteLine("Orders:");
        foreach (var order in orders)
        {
            Console.WriteLine($"- {order.Product}");
        }
    }

    static void PrintTotal(double total)
    {
        Console.WriteLine($"Total: ${total:F2}");
    }
}

// Simple data object
public record Order(string Product, double Price);
