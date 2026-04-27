using UnityEngine;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine.Events;

public class GlobalPlayerHealth : MonoBehaviour
{
    public static GlobalPlayerHealth Instance;

    [SerializeField] private UnityEvent OnDeath;

    public bool IsAlive => _isAlive;
    private bool _isAlive = true;

    [SerializeField] private MeshRenderer _tunnelingVignetteController;
    [SerializeField] private TextMeshProUGUI _tunnelingUGUI;

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
    }

    public void Kill()
    {
        IEnumerator KillVignetteCor()
        {
            Material mtr = _tunnelingVignetteController.material;
            for(int i = 0; i < 100; i++)
            {
                mtr.SetFloat("_ApertureSize", 1 - .01f * i);
                yield return new WaitForSeconds(.01f);
            }
            yield return new WaitForSeconds(.5f);
            _tunnelingUGUI.DOFade(1, 1);
            yield return new WaitForSeconds(5);
            OnDeath.Invoke();
        }

        _isAlive = false;

        StartCoroutine(KillVignetteCor());
    }
}
