using System.Collections;
using TMPro;
using UnityEngine;

public class GlobalFinishController : MonoBehaviour
{
    public static GlobalFinishController Instance;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private PlayerVideoController _videoController;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void DoBadFinish(StepFinalData data)
    {
        IEnumerator TextCor()
        {
            yield return new WaitForSeconds(14);
            _text.text = data.Descryption;
            ScoreCounter.Instance.GetScore(1);
            yield return new WaitForSeconds(10);
            _text.text = "";
        }

        _videoController.Play(false);
        StartCoroutine(TextCor());
    }

    public void DoGoodFinish(StepFinalData data)
    {
        IEnumerator TextCor()
        {
            yield return new WaitForSeconds(14);
            _text.text = data.Descryption;
            ScoreCounter.Instance.GetScore(2);
            yield return new WaitForSeconds(10);
            _text.text = "";
        }

        _videoController.Play(true);
        StartCoroutine(TextCor());
    }
}
