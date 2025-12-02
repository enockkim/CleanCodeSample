using System;
using System.IO;
using System.Collections.Generic;

namespace CleanCodeSample;
class ProcessOrdersDirtyCode
{
    public void ProcessOrders()
    {
        string file = "./Data/orders.csv";
        if (!File.Exists(file))
        {
            Console.WriteLine("File not found");
            return;
        }

        var lines = File.ReadAllLines(file);
        var orders = new List<string>();
        double total = 0;
        bool skippedHeader = false;

        foreach (var line in lines)
        {
            if (!skippedHeader)
            {
                skippedHeader = true;
                continue;
            }

            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(",");
            if (parts.Length < 2) continue;

            string product = parts[0];
            double price = double.Parse(parts[1]);

            orders.Add(product);
            total += price;
        }

        Console.WriteLine("Orders:");
        foreach (var o in orders)
        {
            Console.WriteLine("- " + o);
        }

        Console.WriteLine("Total: $" + total);
    }
}
