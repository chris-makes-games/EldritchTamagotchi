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

    // instance of this script
    // call with Room.instance.whatever_thing_you_want
    public static Room instance;

    // this is the number used to determine what to spawn
    // theoretically this will be grabbed from the quest manager
    // or generated based off of data from the quest manager
    public int roomNumber;
    private int roomNumberPrevFrame;

    // prefabs (this could get bloated very quickly depending on how many prefabs we have)
    public GameObject examplePrefab;
    public GameObject doorTo1;
    public GameObject doorTo2;

    // booleans for room changes
    private bool room1 = false;
    private bool room2 = false;
    private bool startPosition = true;

    // other variables
    public SpriteRenderer floor;



    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // these two if statements check roomNumber to see if it is equal to something
        // probably going to change these to check roomNumber % 10^x
        // which would allow us to use seeds to create layouts
        // (once we have some unique prefabs worth instantiating)

        // at the moment, if roomNumber is 1, spawns a triangle
        // if roomNumber changes from 1, the triangle is destroyed

        // i do not know why the triangle is kinda big (not as big as when it was attached to the floor)

        if (roomNumber != roomNumberPrevFrame) {
            while (transform.childCount > 0) {
                DestroyImmediate(transform.GetChild(0).gameObject);
            room1 = false;
            room2 = false;
            }
        }

        
        if (roomNumber == 1 && room1 == false) {
            // Instantiate(prefabName (need to be declared beforehand), position (Vector3), rotation (quaternion), ref to parent object (this.transform))
            GameObject placeHolder = Instantiate(examplePrefab, Vector3.zero, Quaternion.identity, this.transform);
            Vector3 doorPosition2 = new Vector3(-19.0f, 2.0f, 0.0f);
            GameObject door2 = Instantiate(doorTo2, doorPosition2, Quaternion.identity, this.transform);
            if(!startPosition) PlayerController.instance.transform.position = new Vector3(-17.0f, PlayerController.instance.rb.position.y, 0.0f);
            startPosition = false;
            room1 = true;
        }
        
        else if (roomNumber == 2 && room2 == false) {
            Vector3 doorPosition1 = new Vector3(11.0f, 2.0f, 0.0f);
            GameObject door1 = Instantiate(doorTo1, doorPosition1, Quaternion.identity, this.transform);
            PlayerController.instance.transform.position = new Vector3(9.0f, PlayerController.instance.rb.position.y, 0.0f);
            room2 = true;
        }

        // record roomNumber of previous frame
        // used to check for changes in room number
        roomNumberPrevFrame = roomNumber;
    }
}
