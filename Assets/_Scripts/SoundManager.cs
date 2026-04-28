using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    //this is a singleton object, this line makes the instance static
    private static SoundManager _instance;
    public static SoundManager instance { get { return _instance; } }
    AudioSource player;
    [SerializeField] AudioClip[] voice;

    private void Awake()
    {
        player = GetComponent<AudioSource>();
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SpeakChar(int index)
    {
        StartCoroutine(PlaySound(voice[Random.Range(0, voice.Length - 1)]));
    }

    IEnumerator PlaySound(AudioClip audio)
    {
        player.clip = audio;
        player.loop = false;
        player.Play();
        yield return null;
    }
}
