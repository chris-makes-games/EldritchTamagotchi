using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class StartMenuManager : MonoBehaviour
{
   [SerializeField] private InputActionAsset InputActions;
   private InputAction closeGame;
   private InputAction startGame;

    void Awake()
    {
        // for whatever reason, these inputs are ignored if you come return to this scene from InitScene
        // this is a pretty major bug because it means you can't quit the game after you start it
        // (with controller/keyboard buttons, mouse/trackpad still works with in-game buttons)
        closeGame = InputSystem.actions.FindAction("Pause/Quit");
        startGame = InputSystem.actions.FindAction("Interact/Continue");
    }

    void Start()
    {
        Cursor.visible = true;
        Debug.Log(startGame.ToString());
    }

    // if you push the start button (on screen) it starts the game
    public void StartGame()
    {
        Cursor.visible = false;
        Debug.Log("Starting game...");
        SceneManager.LoadScene("InitScene");
    }

    // if you push the quit button (on screen) it quits the game
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    void Update()
    {
        // closes game on start menu if quit button is pressed (escape or start)
        if (closeGame.WasPressedThisFrame()) QuitGame();

        // starts game if interact button is pressed
        if (startGame.WasPressedThisFrame()) StartGame();
    }
}
