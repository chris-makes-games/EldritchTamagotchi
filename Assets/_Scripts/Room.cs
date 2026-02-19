using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    // HOW DOES THIS WORK/WHAT IS THIS SUPPOSED TO DO
    /*
    *   Given a specific int roomNumber, this will instantiate a set of child objects in the placespace.
    *   Upon changing the number, it kills all of the current children and instantiates a new set of children.
    *   
    *   This will need iteration to accomodate more objects.
    *   New game objects need to be declared in the top here.
    *   If statements need to be setup to check for specific numbers within roomNumber (using roomNumber%10^x)
    *   Also important that we declare some Vector3's in order to place objects in specific positions.
    */


    // this is the number used to determine what to spawn
    // theoretically this will be grabbed from the quest manager
    // or generated based off of data from the quest manager
    public int roomNumber;

    // prefabs (this could get bloated very quickly depending on how many prefabs we have)
    public GameObject examplePrefab;

    // booleans for room changes
    private bool room0 = false;

    // other variables
    public SpriteRenderer floor;

    void Update()
    {
        // these two if statements check roomNumber to see if it is equal to something
        // probably going to change these to check roomNumber % 10^x
        // which would allow us to use seeds to create layouts
        // (once we have some unique prefabs worth instantiating)

        // at the moment, if roomNumber is 0, spawns a triangle
        // if roomNumber changes from 0, the triangle is destroyed

        // i do not know why the triangle is kinda big (not as big as when it was attached to the floor)

        if (roomNumber == 0 && room0 == false) {
            // Instantiate(prefabName (need to be declared beforehand), position (Vector3), rotation (quaternion), ref to parent object (this.transform))
            GameObject placeHolder = Instantiate(examplePrefab, Vector3.zero, Quaternion.identity, this.transform);
            room0 = true;
        }
        if (roomNumber != 0 && room0 == true) {
            Debug.Log(transform.childCount);
            while (transform.childCount > 0) {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
            room0 = false;
        }
    }
}
