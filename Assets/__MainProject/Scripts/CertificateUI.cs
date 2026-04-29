using TMPro;
using UnityEngine;

public class CertificateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name, _score;

    public void UpdateUI(string name, int score)
    {
        _name.text = name;
        _score.text = $"Набрано баллов: {score}/6";
    }
}
