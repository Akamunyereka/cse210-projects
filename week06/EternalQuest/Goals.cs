using System;

public abstract class Goal
{
    // Base class for all goals.
    // Encapsulates shared attributes and behavior.
    private string _title;
    private string _description;
    private int _points; // points awarded when goal is recorded/completed

    protected Goal(string title, string description, int points)
    {
        _title = title;
        _description = description;
        _points = points;
    }

    public string GetTitle()
    {
        return _title;
    }

    public string GetDescription()
    {
        return _description;
    }

    public int GetPoints()
    {
        return _points;
    }

    public void SetPoints(int points)
    {
        _points = points;
    }

    // Return a display string for the goal.
    public abstract string GetDisplayText();

    // When user records progress on this goal, return points actually awarded.
    // Derived classes override to implement appropriate behavior.
    public abstract int RecordEvent();

    // Serialize goal to a string for saving.
    public abstract string Serialize();
}
