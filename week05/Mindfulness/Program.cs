using System;

/*
  Mindfulness Program - Program.cs

  Creativity / Exceeding Requirements (documented here for the rubric):
  - Session logging: each completed activity appends a line to 'mindfulness_log.txt' with timestamp, activity name, and duration.
    This provides a lightweight history/log (exceeds base requirements).
  - The Reflection activity uses variable pause durations (up to 7s per question) to provide short thinking time.
  - Listing activity uses a timed input method so the user can enter items until time expires.

  Note: All classes are in separate files and member variables are private (_underscoreCamelCase).
*/

class Program
{
    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Mindfulness Program ===");
            Console.WriteLine("1) Breathing Activity");
            Console.WriteLine("2) Reflection Activity");
            Console.WriteLine("3) Listing Activity");
            Console.WriteLine("4) Exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();
            switch (choice?.Trim())
            {
                case "1":
                    var breathing = new BreathingActivity();
                    breathing.Run();
                    PromptToContinue();
                    break;
                case "2":
                    var reflection = new ReflectionActivity();
                    reflection.Run();
                    PromptToContinue();
                    break;
                case "3":
                    var listing = new ListingActivity();
                    listing.Run();
                    PromptToContinue();
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid selection. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }

        Console.WriteLine("Goodbye — remember to take a few minutes each day for mindfulness!");
    }

    static void PromptToContinue()
    {
        Console.WriteLine();
        Console.WriteLine("Press Enter to return to the menu.");
        Console.ReadLine();
    }
}
