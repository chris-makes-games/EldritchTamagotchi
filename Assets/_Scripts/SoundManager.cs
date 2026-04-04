using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance; //singleton
    AudioSource player;
    [SerializeField] AudioClip[] alphabet;

    private void Awake()
    {
        player = GetComponent<AudioSource>();
        instance = this;//ensure singleton
    }

    public void SpeakChar(int index)
    {
        player.clip = alphabet[index];
        player.loop = false;
        player.Play();
    }
}
