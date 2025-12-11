using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        activities.Add(new Running("03 Nov 2022", 30, 3.0));
        activities.Add(new Cycling("03 Nov 2022", 30, 6.0));
        activities.Add(new Swimming("03 Nov 2022", 30, 40));

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
