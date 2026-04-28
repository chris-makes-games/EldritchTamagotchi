using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] private GameObject fridgeOpen;

    private void Start()
    {
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
            fridgeOpen.SetActive(true);
        }
        else
        {
            fridgeOpen.SetActive(false);
        }
    }
}
