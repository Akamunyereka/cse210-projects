using System;

public class ChecklistGoal : Goal
{
    private int _targetCount;   // e.g., 10 times
    private int _currentCount;  // how many times recorded so far
    private int _bonusPoints;   // awarded when target reached
    private bool _isCompleted;

    public ChecklistGoal(string title, string description, int pointsPerEntry, int targetCount, int bonusPoints)
        : base(title, description, pointsPerEntry)
    {
        _targetCount = targetCount;
        _currentCount = 0;
        _bonusPoints = bonusPoints;
        _isCompleted = false;
    }

    public override string GetDisplayText()
    {
        string status = _isCompleted ? "[X]" : "[ ]";
        return $"{status} (Checklist) {GetTitle()} - {GetDescription()} (Progress: {_currentCount}/{_targetCount}, Per entry: {GetPoints()} pts, Bonus: {_bonusPoints} pts)";
    }

    // Each time: add per-entry points; when target reached and not yet awarded, also award bonus and mark complete.
    public override int RecordEvent()
    {
        if (_isCompleted)
            return 0;

        _currentCount++;
        int awarded = GetPoints();

        if (_currentCount >= _targetCount)
        {
            _isCompleted = true;
            awarded += _bonusPoints;
        }

        return awarded;
    }

    public int GetCurrentCount()
    {
        return _currentCount;
    }

    public int GetTargetCount()
    {
        return _targetCount;
    }

    public bool IsCompleted()
    {
        return _isCompleted;
    }

    public override string Serialize()
    {
        // Format: Checklist|title|description|pointsPerEntry|target|current|bonus|isCompleted
        return $"Checklist|{Escape(GetTitle())}|{Escape(GetDescription())}|{GetPoints()}|{_targetCount}|{_currentCount}|{_bonusPoints}|{_isCompleted}";
    }

    private string Escape(string s) => s.Replace("|", "¦");
}
