using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour
{
    public Interactable knifeInteractable;
    private bool newVisit = true;
    [SerializeField] private SpriteRenderer knifeSprite;
    [SerializeField] private GameObject knifeObject;
    [SerializeField] private Collider2D trigger;


    // Update is called once per frame
    void Update()
    {
        // okay this shit is kinda crazy
        // partially disappears the knife after the player grabs it (sprite + highlight)
        if (knifeInteractable.visited && newVisit)
        {
            newVisit = false;
            knifeSprite.enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
        // grabs the difference between the player's position and the knife's position
        float posDif = transform.position.x - PlayerController.instance.transform.position.x;

        // disables the trigger for the interaction once the player walks away from the knife
        if ((posDif > 2.0f || posDif < -2.0f) && knifeInteractable.visited)
        {
            trigger.enabled = false;
        }
        // PROBLEMS WITH THIS:
        // 1: the knife is still interactable if the player stands on top of it after grabbing it
        // 1.5: if the player walks away from the knife enough to leave the trigger but not destroy it, it has different interaction text on the (non-existent) knife (semi-intentional)
        // 2: if the player can move away from the knife while interacting, it WILL crash the game (theoretically impossible)

        // there is no good/timely way to remove the interactable part of the knife
        // typically results in endStory() crashing the game by calling something that was disabled/destroyed

    }
}
