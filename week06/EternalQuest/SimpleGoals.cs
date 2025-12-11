using System;

public class SimpleGoal : Goal
{
    private bool _isCompleted;

    public SimpleGoal(string title, string description, int points)
        : base(title, description, points)
    {
        _isCompleted = false;
    }

    public override string GetDisplayText()
    {
        string status = _isCompleted ? "[X]" : "[ ]";
        return $"{status} (Simple) {GetTitle()} - {GetDescription()} (Points: {GetPoints()})";
    }

    // If not completed, mark complete and return points; otherwise return 0.
    public override int RecordEvent()
    {
        if (!_isCompleted)
        {
            _isCompleted = true;
            return GetPoints();
        }
        return 0;
    }

    public bool IsCompleted()
    {
        return _isCompleted;
    }

    public override string Serialize()
    {
        // Format: Simple|title|description|points|isCompleted
        return $"Simple|{Escape(GetTitle())}|{Escape(GetDescription())}|{GetPoints()}|{_isCompleted}";
    }

    private string Escape(string s) => s.Replace("|", "¦");
}
