using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;
    private HashSet<string> _badges = new HashSet<string>();

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public List<Goal> GetGoals()
    {
        return _goals;
    }

    public int GetScore()
    {
        return _score;
    }

    // Record event for a chosen goal index (0-based). Returns points awarded.
    public int RecordGoalEvent(int index)
    {
        if (index < 0 || index >= _goals.Count)
            return 0;

        Goal goal = _goals[index];
        int awarded = goal.RecordEvent();
        _score += awarded;

        // Creativity: simple badge awarding and level-checking
        CheckForBadges(goal, awarded);

        return awarded;
    }

    private void CheckForBadges(Goal goal, int awarded)
    {
        // Badge: First Points Earned
        if (_score >= 1 && !_badges.Contains("Getting Started"))
        {
            _badges.Add("Getting Started");
        }

        // Badge: Completed a checklist
        if (goal is ChecklistGoal checklist)
        {
            if (checklist.IsCompleted() && !_badges.Contains("Checklist Master"))
                _badges.Add("Checklist Master");
        }

        // Badge: Level-based (every 1000 pts)
        int level = GetLevel();
        string levelBadge = $"Level {level}";
        if (!_badges.Contains(levelBadge))
            _badges.Add(levelBadge);
    }

    public int GetLevel()
    {
        // Simple leveling: 1 level per 1000 points
        return Math.Max(1, (_score / 1000) + 1);
    }

    public IEnumerable<string> GetBadges()
    {
        return _badges;
    }

    // Save state to file (including score and goals)
    public void SaveToFile(string path)
    {
        List<string> lines = new List<string>();
        lines.Add($"SCORE|{_score}");
        foreach (Goal goal in _goals)
        {
            lines.Add(goal.Serialize());
        }

        File.WriteAllLines(path, lines);
    }

    // Load state from file (clears existing)
    public void LoadFromFile(string path)
    {
        _goals.Clear();
        _score = 0;
        _badges.Clear();

        if (!File.Exists(path))
            return;

        string[] lines = File.ReadAllLines(path);
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split('|');

            if (parts.Length == 0)
                continue;

            if (parts[0] == "SCORE")
            {
                if (parts.Length >= 2 && int.TryParse(parts[1], out int s))
                    _score = s;
                continue;
            }

            string type = parts[0];

            try
            {
                if (type == "Simple")
                {
                    // Simple|title|description|points|isCompleted
                    string title = Unescape(parts[1]);
                    string desc = Unescape(parts[2]);
                    int points = int.Parse(parts[3]);
                    bool isCompleted = bool.Parse(parts[4]);

                    SimpleGoal g = new SimpleGoal(title, desc, points);
                    if (isCompleted && g.RecordEvent() == points) // mark completed without double-award
                    {
                        // We must mark as completed without awarding points: simpler to set via reflection but avoid that.
                        // Instead: if already completed, we will mark by recording and subtracting points.
                    }
                    // To avoid complications, we reconstruct and track completion by incrementing score if completed:
                    if (isCompleted)
                    {
                        // Mark completed via a special workaround: call RecordEvent only if not completed, but we need point accounting:
                        // We'll mark completed by using a small private helper: simply increment score by points and set internal flag via serialization logic.
                        // Since we can't access private _isCompleted, we instead temporarily create a SimpleGoal and if isCompleted, we will mimic as if recorded and then replace.
                        // Simpler: create the goal and then if isCompleted, add points to score and set the goal as completed by recording and then preventing double award.
                        // We'll implement by creating goal and then using RecordEvent to mark it completed and ensure _score is consistent.
                        int awarded = g.RecordEvent();
                        _score += awarded; // award once to reflect prior completion
                    }

                    _goals.Add(g);
                }
                else if (type == "Eternal")
                {
                    // Eternal|title|description|points
                    string title = Unescape(parts[1]);
                    string desc = Unescape(parts[2]);
                    int points = int.Parse(parts[3]);

                    EternalGoal g = new EternalGoal(title, desc, points);
                    _goals.Add(g);
                }
                else if (type == "Checklist")
                {
                    // Checklist|title|description|pointsPerEntry|target|current|bonus|isCompleted
                    string title = Unescape(parts[1]);
                    string desc = Unescape(parts[2]);
                    int pointsPerEntry = int.Parse(parts[3]);
                    int target = int.Parse(parts[4]);
                    int current = int.Parse(parts[5]);
                    int bonus = int.Parse(parts[6]);
                    bool isCompleted = bool.Parse(parts[7]);

                    ChecklistGoal g = new ChecklistGoal(title, desc, pointsPerEntry, target, bonus);

                    // Replay current times to reach the same state, adding points to score
                    for (int i = 0; i < current; i++)
                    {
                        int awarded = g.RecordEvent();
                        _score += awarded;
                    }

                    _goals.Add(g);
                }
            }
            catch
            {
                // ignore malformed lines
            }
        }

        // After loading, recompute badges based on loaded state
        foreach (Goal goal in _goals)
        {
            // simulate badge checks without modifying score
            if (goal is ChecklistGoal checklist && checklist.IsCompleted())
            {
                _badges.Add("Checklist Master");
            }
        }

        // Add level badge
        _badges.Add($"Level {GetLevel()}");
    }

    private string Unescape(string s) => s.Replace("¦", "|");
}
