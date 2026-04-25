using UnityEngine;
using Ink;

public class NPC : MonoBehaviour
{
    public string nickame;
    public string description;

    [SerializeField] private TextAsset ink;

    public SpriteRenderer icon;
    public SpriteRenderer portrait;
    
    void Start()
    {
        // icon = GetComponent<SpriteRenderer>();
        // portrait = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }
}
