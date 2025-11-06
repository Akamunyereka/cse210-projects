using System;

class Program
{
    static void Main()
    {
        // Ask the user for their grade percentage
        Console.Write("What is your grade percentage? ");
        string input = Console.ReadLine();
        int gradePercentage = int.Parse(input);

        string letter = "";

        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        int lastDigit = gradePercentage % 10;
        string sign = "";

        if (letter != "F" && letter != "A") // F and A have exceptions
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }
        else if (letter == "A")
        {
        
            if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        Console.WriteLine($"Your grade is: {letter}{sign}");

        
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course!");
        }
        else
        {
            Console.WriteLine("Better luck next time! Keep trying!");
        }
    }
}
