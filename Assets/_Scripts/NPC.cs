using Ink.UnityIntegration;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string nickame;
    public string description;

    public InkFile startDialogue;

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
