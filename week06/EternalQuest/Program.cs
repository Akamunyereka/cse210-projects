using System;
using System.Collections.Generic;

/*
  Eternal Quest Program - Program.cs

  Creativity / Exceeding Requirements (documented here as required for rubric):
  - Leveling System: user has a "level" computed from score (1 level per 1000 points).
  - Badges: simple badges awarded for first points ("Getting Started"), completing any checklist ("Checklist Master"), and a Level badge. These encourage "gamification".
  - The above enhancements are lightweight, fully implemented, and explained here.

  Notes:
  - All classes are separate files with appropriate naming and encapsulation.
  - Polymorphism: RecordEvent and GetDisplayText are virtual/abstract in base class and overridden in derived classes.
  - Saving/Loading implemented with a simple line-based serialization.
*/

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        string savePath = "goals.txt";

        // If a saved file exists, prompt to load it automatically
        if (System.IO.File.Exists(savePath))
        {
            Console.WriteLine("A saved goals file was found. Load it? (y/n)");
            string loadChoice = Console.ReadLine().Trim().ToLower();
            if (loadChoice == "y")
            {
                manager.LoadFromFile(savePath);
                Console.WriteLine("Loaded saved goals.");
            }
        }

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n--- Eternal Quest ---");
            Console.WriteLine($"Score: {manager.GetScore()}    Level: {manager.GetLevel()}");
            Console.WriteLine("Badges: " + string.Join(", ", manager.GetBadges()));
            Console.WriteLine("1) Create a new goal");
            Console.WriteLine("2) List goals");
            Console.WriteLine("3) Record an event (complete a goal)");
            Console.WriteLine("4) Save goals");
            Console.WriteLine("5) Load goals");
            Console.WriteLine("6) Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine().Trim();

            switch (choice)
            {
                case "1":
                    CreateGoal(manager);
                    break;
                case "2":
                    ListGoals(manager);
                    break;
                case "3":
                    RecordEvent(manager);
                    break;
                case "4":
                    manager.SaveToFile(savePath);
                    Console.WriteLine($"Saved to {savePath}");
                    break;
                case "5":
                    manager.LoadFromFile(savePath);
                    Console.WriteLine("Loaded from file.");
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        // Auto-save on exit
        manager.SaveToFile(savePath);
        Console.WriteLine($"Progress saved to {savePath}. Goodbye!");
    }

    static void CreateGoal(GoalManager manager)
    {
        Console.WriteLine("\nChoose goal type: 1) Simple  2) Eternal  3) Checklist");
        Console.Write("Type number: ");
        string type = Console.ReadLine().Trim();

        Console.Write("Title: ");
        string title = Console.ReadLine().Trim();
        Console.Write("Description: ");
        string description = Console.ReadLine().Trim();
        Console.Write("Points (integer): ");
        int points = ReadIntFromConsole();

        if (type == "1")
        {
            SimpleGoal g = new SimpleGoal(title, description, points);
            manager.AddGoal(g);
            Console.WriteLine("Simple goal created.");
        }
        else if (type == "2")
        {
            EternalGoal g = new EternalGoal(title, description, points);
            manager.AddGoal(g);
            Console.WriteLine("Eternal goal created.");
        }
        else if (type == "3")
        {
            Console.Write("Target count (how many times to complete to finish): ");
            int target = ReadIntFromConsole();
            Console.Write("Bonus points when target reached: ");
            int bonus = ReadIntFromConsole();
            ChecklistGoal g = new ChecklistGoal(title, description, points, target, bonus);
            manager.AddGoal(g);
            Console.WriteLine("Checklist goal created.");
        }
        else
        {
            Console.WriteLine("Unknown type - cancelled.");
        }
    }

    static void ListGoals(GoalManager manager)
    {
        Console.WriteLine("\n--- Goals ---");
        List<Goal> goals = manager.GetGoals();
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals yet.");
            return;
        }

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {goals[i].GetDisplayText()}");
        }
    }

    static void RecordEvent(GoalManager manager)
    {
        List<Goal> goals = manager.GetGoals();
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals to record. Create one first.");
            return;
        }

        Console.WriteLine("\nSelect a goal to record an event for:");
        for (int i = 0; i < goals.Count; i++)
            Console.WriteLine($"{i + 1}) {goals[i].GetTitle()}");

        int sel = ReadIntFromConsole();
        sel = sel - 1;
        if (sel < 0 || sel >= goals.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        int awarded = manager.RecordGoalEvent(sel);
        if (awarded > 0)
            Console.WriteLine($"Recorded. You earned {awarded} points!");
        else
            Console.WriteLine("No points awarded (maybe the goal was already completed).");
    }

    static int ReadIntFromConsole()
    {
        while (true)
        {
            string input = Console.ReadLine().Trim();
            if (int.TryParse(input, out int value))
                return value;
            Console.Write("Please enter an integer: ");
        }
    }
}
