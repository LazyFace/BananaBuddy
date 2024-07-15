public class EventManager
{
    public delegate void ScoreChangedEvent(int newScore);
    public static event ScoreChangedEvent OnScoreChanged;

    public static void TriggerScoreChanged(int newScore)
    {
        OnScoreChanged?.Invoke(newScore);
    }
}
