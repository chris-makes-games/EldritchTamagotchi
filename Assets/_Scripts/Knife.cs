using UnityEngine;

public class Knife : MonoBehaviour
{
    public Interactable knifeInteractable;
    private bool newVisit = true;
    [SerializeField] private SpriteRenderer knife;


    // Update is called once per frame
    void Update()
    {
        if (knifeInteractable.visited && newVisit)
        {
            newVisit = false;
            knife.enabled = false;
        }
    }
}
