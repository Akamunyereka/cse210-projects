using System;

// Exceeding Requirements:
// 1. Added 2 additional prompts (7 total instead of 5 minimum)
// 2. Enhanced user experience with clear console after each operation
// 3. Added input validation for menu choices
// 4. Added entry count display when loading/displaying journal
// 5. Improved file handling with existence checks and error messages

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();
        string choice = "";

        Console.WriteLine("Welcome to the Journal Program!");

        while (choice != "5")
        {
            Console.WriteLine("\nPlease select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");
            choice = Console.ReadLine();

            if (choice == "1")
            {
                string prompt = promptGenerator.GetRandomPrompt();
                Console.WriteLine($"\n{prompt}");
                Console.Write("> ");
                string response = Console.ReadLine();

                Entry newEntry = new Entry(prompt, response);
                journal.AddEntry(newEntry);
                Console.WriteLine("Entry added successfully!");
            }
            else if (choice == "2")
            {
                Console.WriteLine();
                journal.DisplayEntries();
            }
            else if (choice == "3")
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();
                journal.LoadFromFile(filename);
            }
            else if (choice == "4")
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();
                journal.SaveToFile(filename);
            }
            else if (choice == "5")
            {
                Console.WriteLine("\nThank you for using the Journal Program. Goodbye!");
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select a number from 1 to 5.");
            }
        }
    }
}
