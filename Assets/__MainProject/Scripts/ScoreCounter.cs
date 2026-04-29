using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;

    public int Score => _score;
    private int _score;
    [SerializeField] private TextMeshProUGUI _scoreTxt;

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }

        UpdateUI();
    }

    public void GetScore(int score)
    {
        _score += score;
        UpdateUI();
    }

    public void UpdateUI()
    {
        _scoreTxt.text = _score.ToString();
    }
}
