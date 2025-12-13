using System;
using System.Diagnostics;
using System.Threading;

public class BreathingActivity : Activity
{
    private int _inhaleSeconds;
    private int _exhaleSeconds;

    // Inhale/exhale timing can be tuned. Kept private to satisfy encapsulation.
    public BreathingActivity()
        : base("Breathing Activity",
               "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
        _inhaleSeconds = 4;
        _exhaleSeconds = 6;
    }

    protected override void ExecuteActivity()
    {
        int duration = GetDurationSeconds();
        Stopwatch sw = Stopwatch.StartNew();

        Console.WriteLine();
        Console.WriteLine("Follow the prompts and breathe slowly.");
        Console.WriteLine();

        while (sw.Elapsed.TotalSeconds < duration)
        {
            // Breathe in
            Console.Write("Breathe in... ");
            int inhale = Math.Min(_inhaleSeconds, Math.Max(1, duration - (int)sw.Elapsed.TotalSeconds));
            CountdownWithSpinner(inhale);
            Console.WriteLine();

            if (sw.Elapsed.TotalSeconds >= duration) break;

            // Breathe out
            Console.Write("Breathe out... ");
            int exhale = Math.Min(_exhaleSeconds, Math.Max(1, duration - (int)sw.Elapsed.TotalSeconds));
            CountdownWithSpinner(exhale);
            Console.WriteLine();
        }
    }

    // Countdown showing numbers each second with a small spinner while counting
    private void CountdownWithSpinner(int seconds)
    {
        for (int i = seconds; i >= 1; i--)
        {
            Console.Write(i);
            PauseWithSpinner(1); // spinner for 1 second (4 frames)
            Console.Write("\b \b");
        }
    }
}
