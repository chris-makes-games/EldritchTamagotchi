using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] private GameObject fridgeOpen;
    private SpriteRenderer fridgeClosed;

    private void Start()
    {
        fridgeClosed = GetComponent<SpriteRenderer>();
        fridgeOpen.SetActive(false);
    }

    private void OnEnable()
    {
        InkManager.FridgeEvent += fridgeChange;
    }

    private void OnDisable()
    {
        InkManager.FridgeEvent -= fridgeChange;
    }

    private void fridgeChange(bool open)
    {
        if (open)
        {
            fridgeClosed.enabled = false;
            fridgeOpen.SetActive(true);
        }
        else
        {
            fridgeClosed.enabled = true;
            fridgeOpen.SetActive(false);
        }
    }
}
