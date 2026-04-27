using UnityEngine;
using TMPro;

public class GlobalTimer : MonoBehaviour
{
    [SerializeField] private float _timeValue = 180;
    [SerializeField] private TextMeshProUGUI _timerText;

    void Update()
    {
        if (_timeValue > 0)
        {
            _timeValue -= Time.deltaTime;
        }
        else
        {
            _timeValue = 0;
        }

        DisplayTime(_timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _timerText.text = string.Format("До конца смены:\n{0:00}:{1:00}", minutes, seconds);
    }
}
