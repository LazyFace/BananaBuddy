using UnityEngine;

public class ScoreManager : MonoBehaviour, IObserver
{
    private int score = 0;
    [SerializeField] private int scorePerBanana = 1;

    private BananaCollector[] bananas;

    private void Start()
    {
        bananas = FindObjectsOfType<BananaCollector>(true);

        foreach(BananaCollector banana in bananas)
        {
            banana.Subscribe(this);
        }
    }

    private void OnDestroy()
    {
        foreach (BananaCollector banana in bananas)
        {
            banana.Unsubscribe(this);
        }
    }

    public void Notify(Actions action)
    {
        if(action == Actions.BananaPicked)
        {
            AddScore(scorePerBanana);
        }
    }

    private void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        EventManager.TriggerScoreChanged(score);
    }
}
