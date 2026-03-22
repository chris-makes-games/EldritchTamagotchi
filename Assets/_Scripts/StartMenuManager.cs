using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class StartMenuManager : MonoBehaviour
{
   [SerializeField] private InputActionAsset InputActions;
   [SerializeField] private InputAction closeGame;
   [SerializeField] private InputAction startGame;

    void Awake()
    {
        closeGame = InputSystem.actions.FindAction("Pause/Quit");
        startGame = InputSystem.actions.FindAction("Interact/Continue");
    }

    void Start()
    {
        Cursor.visible = true;
    }

    // if you push the start button (on screen) it starts the game
    public void StartGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("InitScene");
        Debug.Log("Launching game...");
    }

    void Update()
    {
        // closes game on start menu if quit button is pressed (escape or start)
        if (closeGame.WasPressedThisFrame())
        {
            Debug.Log("Quitting game...");
            Application.Quit();
        }

        if (startGame.WasPressedThisFrame()) StartGame();
    }
}
