using UnityEngine;

public class GlobalFinishController : MonoBehaviour
{
    public static GlobalFinishController Instance;

    [SerializeField] private PlayerVideoController _videoController;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void DoBadFinish()
    {
        _videoController.Play(false);
    }

    public void DoGoodFinish()
    {
        _videoController.Play(true);
    }
}
