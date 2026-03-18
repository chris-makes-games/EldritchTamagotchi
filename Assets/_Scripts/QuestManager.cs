using UnityEngine;
using UnityEngine.Rendering;

public class QuestManager : MonoBehaviour
{
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


    // Called on game launch to make sure there's only one controller
    void Awake()
    {
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

    
}
