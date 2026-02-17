using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //this is a singleton object, this line makes the instance static
    public static QuestManager instance;

    //variables to keep track of game state
    private float love;
    private int questsCompleted;


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
}
