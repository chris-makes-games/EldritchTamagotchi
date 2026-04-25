using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorSceneChanger : MonoBehaviour
{
    // input name of the scene to swap to in the editor itself
    // can have different things in different scenes
    [SerializeField] private string sceneToLoad;
    public bool doorLocked;
    private int doorCooldown;
    public static DoorSceneChanger instance;

    void Awake()
    {
        // allows you to call this script and change door from locked/unlocked
        instance = this;
    }

    void Start()
    {
        doorCooldown = 0;
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
        if (!doorLocked && doorCooldown >= 60) {
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("Loading " + sceneToLoad + "...");
        }
    }
}
