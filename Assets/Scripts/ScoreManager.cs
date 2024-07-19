using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour, IObserver
{
    private int score = 0;
    [SerializeField] private int scorePerBanana = 1;

    private BananaCollector[] bananas;

    [SerializeField] private UnityEvent AllBananasCollected;

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

        if(score == bananas.Length)
        {
            AllBananasCollected?.Invoke();
            StartCoroutine(WaitToClose());
        }
    }

    private IEnumerator WaitToClose()
    {
        yield return new WaitForSeconds(5f);

        Application.Quit();
    }
}
