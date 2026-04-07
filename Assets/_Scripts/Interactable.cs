using UnityEngine;

public class Interactable : MonoBehaviour
{
    private SpriteRenderer sprite;
    private SpriteRenderer highlight;
    public float highlightSize = 1.3f;

    //choice outcomes for quests - complete quests or not
    [SerializeField] public bool[] outcomes;

    //ink file for text
    [SerializeField] private TextAsset ink;

    //to say something about this before the interaction
    [SerializeField] public string description;

    //check if this interactable has been performed already
    public bool visited = false;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            highlight.enabled = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            highlight.enabled = false;
        }
    }

    public void ToggleHighlight()
    {
        if (highlight.enabled)
        {
            highlight.enabled = false;
        }
        else
        {
            highlight.enabled = true;
        }
    }

    public void Visit()
    {
        visited = true;
    }

    public TextAsset GetInk() 
    { 
        return ink;
    }
}
