using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSceneChanger : MonoBehaviour
{
    // input name of the scene to swap to in the editor itself
    // can have different things in different scenes
    [SerializeField] private string sceneToLoad;
    public bool doorLocked;
    private int doorCooldown;
    public static DoorSceneChanger instance;

    [SerializeField] private QuestManager questManager;

    //use this to call fade in and out
    [SerializeField] private SleepManager sleepManager;

    void Awake()
    {
        // allows you to call this script and change door from locked/unlocked
        instance = this;
    }

    void Start()
    {
        doorCooldown = 0;
    }

    private void OnEnable()
    {
        InkManager.HatEvent += HatEvent;
    }

    private void OnDisable()
    {
        InkManager.HatEvent -= HatEvent;
    }

    private void HatEvent(int hatEvent)
    {
        //hatEvent int
        // 0 - not tried yet
        // 1 - correct hat
        // 2 - incorrect hat
        if (hatEvent == 1)
        {
            sceneToLoad = "UnderRoom";
            doorLocked = false;
            Debug.Log("Door unlocked (Underachiever)");
        }
        else if (hatEvent == 2)
        {
            // change to OverRoom once you make that
            sceneToLoad = "OverRoom";
            doorLocked = false;
            Debug.Log("Door unlocked (Overachiever)");
        }
    }

    void FixedUpdate() 
    {
        if (doorCooldown < 60)
        {
            doorCooldown++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!doorLocked && doorCooldown >= 60 && collision.CompareTag("Player")) {
            Debug.Log("Loading " + sceneToLoad + "...");
            StartCoroutine(questManager.LoadScene(sceneToLoad));
        }
    }
}
