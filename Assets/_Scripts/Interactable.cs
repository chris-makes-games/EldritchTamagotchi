using Ink.UnityIntegration;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    private SpriteRenderer sprite;
    private SpriteRenderer highlight;
    private bool highlighted;
    public float highlightSize;

    //interact button
    public InputActionAsset InputActions;
    public InputAction interact;

    //ink file for text
    [SerializeField] private TextAsset ink;

    void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void Awake()
    {
        interact = InputSystem.actions.FindAction("Interact/Continue");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //creates a copy of the initial sprite and makes a highlighter out of it
        sprite = GetComponent<SpriteRenderer>();

        //nned to create an empty gameobject because cannot clone a spriterenderer
        GameObject highlightObj = new GameObject("highlight");
        highlightObj.transform.SetParent(transform);
        highlightObj.transform.localPosition = Vector3.zero;

        //adds a spriterenderer to that object
        highlight = highlightObj.AddComponent<SpriteRenderer>();

        //sets the sprite as a copy of original sprite, copies the layer - 1
        highlight.sprite = sprite.sprite;
        highlight.sortingLayerID = sprite.sortingLayerID;
        highlight.sortingOrder = sprite.sortingOrder - 1;

        //makes the highlight a little bigger
        highlight.transform.localScale *= highlightSize;

        //light yellow color
        highlight.color = Color.lightGoldenRod;

        //disables the highlight on start
        highlight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (highlighted && interact.WasPressedThisFrame())
        {
            Debug.Log(ink.text);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            highlight.enabled = true;
            highlighted = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            highlight.enabled = false;
            highlighted = false;
        }
    }
}
