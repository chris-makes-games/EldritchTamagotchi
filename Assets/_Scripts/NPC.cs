using UnityEngine;
using Ink;

public class NPC : MonoBehaviour
{
    public Interactable npcInteractable;

    void Update()
    {
        // if NPC is visited, door unlocks
        if (npcInteractable.visited)
        {
            DoorSceneChanger.instance.doorLocked = false;
        }
    }
}
