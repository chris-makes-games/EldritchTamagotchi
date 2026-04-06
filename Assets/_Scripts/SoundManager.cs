using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance; //singleton
    AudioSource player;
    [SerializeField] AudioClip[] voice;
    [SerializeField] AudioClip error;
    [SerializeField] AudioClip select;

    private void Awake()
    {
        player = GetComponent<AudioSource>();
        instance = this;//ensure singleton
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
