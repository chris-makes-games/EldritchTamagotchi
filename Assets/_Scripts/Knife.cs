using UnityEngine;

public class Knife : MonoBehaviour
{
    public Interactable knifeInteractable;
    private bool newVisit = true;
    [SerializeField] private GameObject knife;


    // Update is called once per frame
    void Update()
    {
        if (knifeInteractable.visited && newVisit)
        {
            newVisit = false;
            knife.SetActive(false);
        }
    }
}
