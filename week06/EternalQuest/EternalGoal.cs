using System;

public class EternalGoal : Goal
{
    // Eternal goals are never "completed"; each recording gives points.
    public EternalGoal(string title, string description, int points)
        : base(title, description, points)
    {
    }

    public override string GetDisplayText()
    {
        return $"[∞] (Eternal) {GetTitle()} - {GetDescription()} (Per entry: {GetPoints()} pts)";
    }

    public override int RecordEvent()
    {
        return GetPoints();
    }

    public override string Serialize()
    {
        // Format: Eternal|title|description|points
        return $"Eternal|{Escape(GetTitle())}|{Escape(GetDescription())}|{GetPoints()}";
    }

    private string Escape(string s) => s.Replace("|", "¦");
}
