using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class PlayerVideoController : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;

    [SerializeField] private VideoPlayer _player;
    [SerializeField] private VideoClip _lose, _win;

    IEnumerator PlaybackVignetteCor(bool isWin)
    {
        Material mtr = _renderer.material;
        for (int i = 0; i < 100; i++)
        {
            mtr.SetFloat("_ApertureSize", 1 - .01f * i);
            yield return new WaitForSeconds(.01f);
        }
        yield return new WaitForSeconds(1.5f);
        _player.gameObject.SetActive(true);
        _player.clip = isWin ? _win : _lose;
        _player.Play();
    }

    public void Play(bool isWin)
    {
        StartCoroutine(PlaybackVignetteCor(isWin));
    }
}
