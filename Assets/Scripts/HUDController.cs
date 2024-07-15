using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        EventManager.OnScoreChanged += UpdateScoreUI;
    }

    private void OnDestroy()
    {
        EventManager.OnScoreChanged -= UpdateScoreUI;
    }

    private void UpdateScoreUI(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = newScore.ToString();
        }
    }
}
