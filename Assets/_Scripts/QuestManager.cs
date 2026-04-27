using UnityEngine;
using TMPro;
using System.Collections;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private InkManager inkManager;

    //to be able to turn off the currently talking text if necessary
    [SerializeField] private SoundManager soundManager;
    private AudioSource talkingAudio;

    //this is a singleton object, this line makes the instance static
    public static QuestManager instance;

    //variables to keep track of game state
    private float love;
    public float loveIncrement; //how much to increase love per quest
    public float loveDecrement; //how much to decrease
    private int questsCompleted;
    private int questsFailed;

    //list of quests that have not been completed
    private Quest[] activeQuests;

    //for the caretaker text display
    [SerializeField] private GameObject careTakerPanel;
    [SerializeField] private TextMeshProUGUI careTakerText;

    //to scream the characters
    AudioSource soundSource;
    [SerializeField] AudioClip[] screams;

    //delay between characters
    [SerializeField] float delay = 0.025f;

    //so it doesn't yell straight away
    [SerializeField] float waitToYell;


    // Called on game launch to make sure there's only one controller
    void Awake()
    {
        careTakerText.text = ""; //start empty of text
        soundSource = GetComponent<AudioSource>();
        talkingAudio = soundManager.GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }

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
        talkingAudio.mute = true; //stops audio
        StartCoroutine(SlowText(text));
    }

    public void ClearQuestText()
    {
        careTakerText.text = "";
    }

    public void ScreamChar(int index)
    {
        StartCoroutine(PlaySound(screams[Random.Range(0, screams.Length - 1)]));
    }

    IEnumerator PlaySound(AudioClip audio)
    {
        soundSource.clip = audio;
        soundSource.loop = false;
        soundSource.Play();
        yield return null;
    }

    IEnumerator SlowText(string text) //waits for the delay in-between characters of the given text
    {
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
