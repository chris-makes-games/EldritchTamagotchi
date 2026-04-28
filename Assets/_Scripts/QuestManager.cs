using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private InkManager inkManager;

    [SerializeField] private SleepManager sleepManager;

    //to be able to turn off the currently talking text if necessary
    [SerializeField] private SoundManager soundManager;
    private AudioSource talkingAudio;

    //this is a singleton object, this line makes the instance static
    private static QuestManager _instance;
    public static QuestManager instance { get { return _instance; } }

    //variables to keep track of game state
    private float love;
    public float loveIncrement; //how much to increase love per quest
    public float loveDecrement; //how much to decrease

    //list of quests that have not been completed
    private Quest[] activeQuests;

    //for the caretaker text display
    [SerializeField] private GameObject careTakerPanel;
    [SerializeField] private TextMeshProUGUI careTakerText;
    private string currentText = "";

    //to scream the characters
    AudioSource soundSource;
    [SerializeField] AudioClip scream;

    //delay between characters
    [SerializeField] float delay = 0.025f;

    //so it doesn't yell straight away
    [SerializeField] float waitToYell;

    //unity event: I am a wizard - Chris
    public static event Action<QuestManager> SceneLoadEvent;


    // Called on game launch to make sure there's only one controller
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        careTakerText.text = ""; //start empty of text
        soundSource = GetComponent<AudioSource>();
        talkingAudio = soundManager.GetComponent<AudioSource>();
    }

    public void BeginQuest(Quest newQuest)
    {
        //need to make a new list
        //proabbaly a bad idea, but won't happen often
        Quest[] newQuests = new Quest[activeQuests.Length + 1];
        for (int i = 0; i < newQuests.Length; i++)
        {
            newQuests[i] = activeQuests[i];
        }
        newQuests[newQuests.Length] = newQuest;
        activeQuests = newQuests;
    }

    public void SetQuestText(string text)
    {
        ClearQuestText();
        currentText = text;
        talkingAudio.mute = true; //stops audio
        StartCoroutine(SlowText(text));
    }

    public IEnumerator TempText(string text)
    {
        StopAllCoroutines();
        string oldText = currentText;
        yield return StartCoroutine(SlowText(text));
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(SlowText(oldText));
    }

    public IEnumerator ChainText(List<string> phrases)
    {
        StopAllCoroutines();
        foreach ( string phrase in phrases)
        {
            yield return StartCoroutine(SlowText(phrase));
        }
    }

    public void ClearQuestText()
    {
        careTakerText.text = "";
    }

    public void ScreamChar(int index)
    {
        StartCoroutine(PlaySound(scream));
    }

    IEnumerator PlaySound(AudioClip audio)
    {
        soundSource.clip = audio;
        soundSource.loop = false;
        soundSource.pitch = Random.Range(0.7f, 1.1f);
        soundSource.Play();
        yield return null;
    }

    public IEnumerator LoadScene(string sceneName)
    {
        SceneLoadEvent?.Invoke(this);
        yield return StartCoroutine(sleepManager.FadeToBlack());
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator SlowText(string text) //waits for the delay in-between characters of the given text
    {
        currentText = text;
        yield return new WaitForSeconds(waitToYell);
        talkingAudio.mute = false;
        string output = "";
        for (int i = 0; i < text.Length; i++)
        {
            output = output + char.ToUpper(text[i]);
            careTakerText.text = output;
            int index = char.ToUpper(text[i]) - 65; //turns char to position in alphabet
            if (index >= 0 && index < 27)
            {
                ScreamChar(index);
            }
            yield return new WaitForSeconds(delay);
        }
    }


}
