using UnityEngine;

public class GameManager : MonoBehaviour
{
    //this is a singleton object, this line makes the instance static
    public static GameManager instance;

    //variables to keep track of game state
    private float love;
    private int questsCompleted;
    private int playerHealth;


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
