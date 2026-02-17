using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    // this is the number used to determine what to spawn
    // theoretically this will be grabbed from the quest manager
    // rather than being grabbed from here
    public int roomNumber;

    // prefab list (using lists so we don't have to worry about resizing)
    List<GameObject> prefabList = new List<GameObject>();

    // prefabs (this could get bloated very quickly 
    // depending on how many prefabs we have)
    public GameObject examplePrefab;

    private bool room0 = false;
    
    void Start()
    {
        if (roomNumber == 0) {
            GameObject placeHolder = Instantiate(examplePrefab, Vector3.zero, Quaternion.identity);
            prefabList.Add(Instantiate(examplePrefab, Vector3.zero, Quaternion.identity));
            
        }
    }

    void Update()
    {
        if (roomNumber == 0 && room0 == false) {
            GameObject placeHolder = Instantiate(examplePrefab, Vector3.zero, Quaternion.identity);
            room0 = true;
        }
        // this part does not work yet, needs some fiddling
        if (roomNumber != 0 && room0 == true) {
            Destroy(prefabList[0]);
            prefabList.Clear();
        }
    }
}
