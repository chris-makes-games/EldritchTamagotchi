using UnityEngine;

public class Door1_2 : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Room.instance.roomNumber = 2;
    }
}
