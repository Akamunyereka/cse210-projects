using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<double> numbers = new List<double>();
        Console.WriteLine("Enter numbers one at a time. Enter 0 to finish (0 will not be added).");

        while (true)
        {
            Console.Write("Enter a number (0 to stop): ");
            string input = Console.ReadLine();

            // Validate parse
            if (!double.TryParse(input, out double value))
            {
                Console.WriteLine("That's not a valid number. Please try again.");
                continue;
            }

            // Stop condition (do not add 0 to the list)
            if (value == 0)
                break;

            numbers.Add(value);
        }

        // Handle empty list
        if (numbers.Count == 0)
        {
            Console.WriteLine("\nNo numbers were entered.");
            return;
        }

        // Core computations
        double sum = numbers.Sum();
        double average = sum / numbers.Count;
        double maximum = numbers.Max();

        // Output results
        Console.WriteLine("\nResults:");
        Console.WriteLine($"Count:   {numbers.Count}");
        Console.WriteLine($"Sum:     {sum}");
        Console.WriteLine($"Average: {average}");
        Console.WriteLine($"Maximum: {maximum}");
    }
}
