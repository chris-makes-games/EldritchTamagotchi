using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int roomToSpawn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Room.instance.roomNumber = roomToSpawn;
    }
}
