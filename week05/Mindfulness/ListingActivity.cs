using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>()
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private Random _random = new Random();

    public ListingActivity()
        : base("Listing Activity",
               "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    { }

    protected override void ExecuteActivity()
    {
        int duration = GetDurationSeconds();
        Stopwatch sw = Stopwatch.StartNew();

        string prompt = _prompts[_random.Next(_prompts.Count)];
        Console.WriteLine();
        Console.WriteLine("List as many responses as you can to the prompt below:");
        Console.WriteLine($"--- {prompt} ---");
        Console.WriteLine();

        Console.WriteLine("You will have a short moment to think before you begin...");
        Countdown(5);
        Console.WriteLine();
        Console.WriteLine("Start listing items. Press ENTER after each item.");

        List<string> items = new List<string>();

        while (sw.Elapsed.TotalSeconds < duration)
        {
            int remaining = duration - (int)sw.Elapsed.TotalSeconds;
            if (remaining <= 0) break;

            Console.Write($"(Time left: {remaining}s) > ");

            // ReadLine with timeout using Task
            Task<string> readTask = Task.Run(() => Console.ReadLine() ?? "");
            bool completed = readTask.Wait(TimeSpan.FromSeconds(remaining));

            if (!completed)
            {
                // time expired while waiting for input
                break;
            }

            string entry = readTask.Result.Trim();
            if (!string.IsNullOrEmpty(entry))
                items.Add(entry);
        }

        Console.WriteLine();
        Console.WriteLine($"Well done — you listed {items.Count} items!");
        if (items.Count > 0)
        {
            Console.WriteLine("Here are your items:");
            foreach (string it in items)
                Console.WriteLine($" - {it}");
        }
    }
}
