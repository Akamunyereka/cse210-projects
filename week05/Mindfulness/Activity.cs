using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

public abstract class Activity
{
    private string _name;
    private string _description;
    private int _durationSeconds;

    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _durationSeconds = 0;
    }

    // Entry point for running any activity.
    public void Run()
    {
        ShowStartMessage();
        PrepareToBegin();
        ExecuteActivity();
        ShowEndMessage();
        LogSession();
    }

    // Display name/description and ask for duration (shared behavior).
    protected virtual void ShowStartMessage()
    {
        Console.Clear();
        Console.WriteLine($"--- {_name} ---");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("Enter duration in seconds: ");
        _durationSeconds = ReadPositiveIntFromConsole();
    }

    // Small pause with dots before activity starts.
    protected void PrepareToBegin()
    {
        Console.WriteLine();
        Console.WriteLine("Get ready...");
        PauseWithDots(3);
    }

    // Each derived class implements the main behavior.
    protected abstract void ExecuteActivity();

    // Shared ending message and short pause.
    protected virtual void ShowEndMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        PauseWithDots(2);
        Console.WriteLine($"You have completed the {_name} for {_durationSeconds} seconds.");
        PauseWithDots(3);
    }

    // Utility: show spinner for given seconds.
    protected void PauseWithSpinner(int seconds)
    {
        string[] spinner = new string[] { "|", "/", "-", "\\" };
        int iterations = Math.Max(1, seconds * 4); // 250ms per frame
        for (int i = 0; i < iterations; i++)
        {
            Console.Write(spinner[i % spinner.Length]);
            Thread.Sleep(250);
            Console.Write("\b");
        }
    }

    // Utility: show dots animation for given seconds.
    protected void PauseWithDots(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    // Utility: countdown from given seconds with visible numbers.
    protected void Countdown(int seconds)
    {
        for (int i = seconds; i >= 1; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    // Get how many seconds the user requested
    protected int GetDurationSeconds()
    {
        return _durationSeconds;
    }

    // Logging creativity: append a line to a simple log file
    private void LogSession()
    {
        try
        {
            string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {_name} | {_durationSeconds} seconds";
            File.AppendAllLines("mindfulness_log.txt", new[] { logLine });
        }
        catch
        {
            // non-fatal if logging fails
        }
    }

    private int ReadPositiveIntFromConsole()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int value) && value > 0)
                return value;
            Console.Write("Please enter a positive integer: ");
        }
    }
}
