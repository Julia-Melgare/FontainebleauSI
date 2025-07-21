using UnityEngine;
using UnityEngine.Events;

public class MusicController : MonoBehaviour
{
    public AudioSource preludeAudioTrack;
    public AudioSource[] audioTracks;

    public UnityEvent onFlutePlayOnce;
    public UnityEvent onFlutePlayTwice;
    private bool isPlayingMusic = false;
    private bool flutePlayedOnce = false;
    private bool flutePlayedTwice = false;
    void Start()
    {
        preludeAudioTrack.Play();
    }

    void Update()
    {
        if (preludeAudioTrack.time >= 118f && !isPlayingMusic)
        {
            foreach (var t in audioTracks)
            {
                t.Play();
            }

            isPlayingMusic = true;
        }

        if (audioTracks[3].time >= 64f && !flutePlayedOnce)
        {
            onFlutePlayOnce.Invoke();
            flutePlayedOnce = true;
        }
        
        if (audioTracks[3].time >= 374f && !flutePlayedTwice)
        {
            onFlutePlayTwice.Invoke();
            flutePlayedTwice = true;
        }

    }
}
