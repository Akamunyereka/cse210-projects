using System;

public abstract class Activity
{
    private string _date;
    private int _lengthMinutes;

    public Activity(string date, int lengthMinutes)
    {
        _date = date;
        _lengthMinutes = lengthMinutes;
    }

    protected int GetLengthMinutes()
    {
        return _lengthMinutes;
    }

    protected string GetDate()
    {
        return _date;
    }

    // ABSTRACT METHODS TO BE OVERRIDDEN
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // BASE SUMMARY METHOD (uses polymorphism)
    public virtual string GetSummary()
    {
        return $"{_date} {GetType().Name} ({_lengthMinutes} min) - " +
               $"Distance {GetDistance():0.0} miles, " +
               $"Speed {GetSpeed():0.0} mph, " +
               $"Pace: {GetPace():0.0} min per mile";
    }
}
