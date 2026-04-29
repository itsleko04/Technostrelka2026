using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using static UnityEditor.Experimental.GraphView.GraphView;

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
        yield return new WaitForSeconds(10);
        _player.Stop();
        _player.clip = null;
        _player.gameObject.SetActive(false);
        yield return new WaitForSeconds(12);
        mtr.SetFloat("_ApertureSize", 1);
    }

    public void Play(bool isWin)
    {
        StartCoroutine(PlaybackVignetteCor(isWin));
    }
}
