using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    private const string TurretsDestroyed = "Turrets destroyed: ";

    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        _scoreCounter.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
       _score.text = TurretsDestroyed + score.ToString();
    }
}
