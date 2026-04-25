using UnityEngine;
using Ink;

public class NPC : MonoBehaviour
{
    public Interactable npcInteractable;
    private bool newVisit = true;

    void Update()
    {
        // if NPC is visited, door unlocks
        if (npcInteractable.visited && newVisit)
        {
            DoorSceneChanger.instance.doorLocked = false;
            newVisit = false;
        }
    }
}
