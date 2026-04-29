using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerRegisterController : MonoBehaviour
{
    public static PlayerRegisterController Instance;

    [SerializeField] private List<GameObject> _arrows;

    [SerializeField] private CertificateUI _certificate;

    [SerializeField] private TMP_InputField _nameInput;
    private string _playerName;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void GetPlayerName()
    {
        _playerName = _nameInput.text;
        print(_playerName);
    }

    public void OnArrowDisabled()
    {
        bool allDisabled = true;
        foreach (GameObject arrow in _arrows)
        {
            if(arrow.activeSelf)
                allDisabled = false;
        }
        if (allDisabled)
            UpdateCertificateUI();
    }

    public void UpdateCertificateUI()
    {
        IEnumerator ShowCor()
        {
            yield return new WaitForSeconds(30);
            _certificate.gameObject.SetActive(true);
            _certificate.UpdateUI(_playerName, ScoreCounter.Instance.Score);
        }

        StartCoroutine(ShowCor());
    }
}
