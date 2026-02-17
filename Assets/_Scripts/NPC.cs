using UnityEngine;
using Ink;

public class NPC : MonoBehaviour
{
    public string nickame;
    public string description;

    [SerializeField] private TextAsset ink;

    public SpriteRenderer icon;
    public SpriteRenderer portrait;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon = GetComponent<SpriteRenderer>();
        portrait = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //maybe we can have animations play here, or have them wander
    }
}
