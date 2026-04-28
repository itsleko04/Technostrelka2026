using TMPro;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WaitForMessageIdle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private string _baseText = "Loading";
    [SerializeField] private float _delay = .5f;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        StartAnim();
    }

    public void StartAnim()
    {
        StartCoroutine(AnimateText());
    }

    public void StopAnim()
    {
        _text.text = _baseText;
        StopAllCoroutines();
    }

    IEnumerator AnimateText()
    {
        while (true)
        {
            _text.text = _baseText + ".";
            yield return new WaitForSeconds(_delay);
            _text.text = _baseText + "..";
            yield return new WaitForSeconds(_delay);
            _text.text = _baseText + "...";
            yield return new WaitForSeconds(_delay);
        }
    }
}
