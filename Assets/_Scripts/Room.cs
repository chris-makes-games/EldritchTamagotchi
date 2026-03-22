using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    // HOW DOES THIS WORK/WHAT IS THIS SUPPOSED TO DO
    /*
    *   Given a specific int roomNumber, this will instantiate a set of child objects in the playspace.
    *   Upon changing that number, it kills all of the current children and instantiates a new set of children.
    *   
    *   This will need iteration to accomodate more objects.
    *   New game objects need to be declared in the top here.
    *   (i know this is a lot of variables)
    */

    // HOW TO ADD/CHANGE THINGS (rooms or objects) (i know this is kinda tedious, sorry)
    /*
    *   adding a room:
    *   create a boolean named "roomx" with x being the number of that room
    *   in update(), do this:
    *   if (roomNumber == x && roomx == false) {
    *   // all the code for adding things goes here
    *   roomx = true;
    *   }
    *
    *   adding objects to a room:
    *   add a new item to the list of prefabs seen below (public GameObject nameOfObject)
    *   locate the room you want to spawn the object in and write this:
    *   GameObject thisNameDoesNotMatter = Instantiate(nameOfObject, position, Quaternion.identity, this.transform);
    *   for position, either create a Vector3 or use the presets from below
    *   don't mess with the quaternion part or the transform part
    */

    // instance of this script
    // call with Room.instance.whatever_thing_you_want
    public static Room instance;

    // this is the number used to determine what to spawn
    // theoretically this will be grabbed/generated from the quest or dialogue managers
    public int roomNumber;
    private int roomNumberPrevFrame;

    // prefabs (all of the objects that the room manager can spawn)
    public GameObject examplePrefab; // used in everything, replace with bespoke prefabs when we make them
    public GameObject dog;
    public GameObject doorTo1;
    public GameObject doorTo2;

    // potential spawn locations (probably won't use all of these, but nice to have)
    private Vector3 topLeft = new Vector3(-16.0f, 8.0f, 0.0f);
    private Vector3 topMiddle = new Vector3(-4.0f, 8.0f, 0.0f);
    private Vector3 topRight = new Vector3(8.0f, 8.0f, 0.0f);
    private Vector3 middleLeft = new Vector3(-16.0f, 3.0f, 0.0f); // watch out for doors
    private Vector3 middle = new Vector3(8.0f, 3.0f, 0.0f);
    private Vector3 middleRight = new Vector3(-16.0f, 3.0f, 0.0f); // watch out for doors
    private Vector3 bottomLeft = new Vector3(-16.0f, -2.5f, 0.0f);
    private Vector3 bottomMiddle = new Vector3(-4.0f, -2.5f, 0.0f);
    private Vector3 bottomRight = new Vector3(8.0f, -2.5f, 0.0f);
    private Vector3 doorLeft = new Vector3(-19.0f, 2.0f, 0.0f); // used for doors
    private Vector3 doorRight = new Vector3(11.0f, 2.0f, 0.0f);

    // booleans for room changes
    private bool room1 = false;
    private bool room2 = false;
    private bool startPosition = true;

    // other variables
    public SpriteRenderer floor;
    public Color floorBlue, floorGreen; // replace with sprites when we have floor assets

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // these if statements check roomNumber to see if it should spawn/destroy anything

        // i do not know why the triangle is kinda big


        // kills all objects when room number changes
        if (roomNumber != roomNumberPrevFrame) {
            while (transform.childCount > 0) {
                DestroyImmediate(transform.GetChild(0).gameObject);
            room1 = false;
            room2 = false;
            }
        }

        // spawns room 1 if roomNumber is 1
        if (roomNumber == 1 && room1 == false) {
            // Instantiate(prefabName (need to be declared beforehand), position (Vector3), rotation (quaternion), ref to parent object (this.transform))
            GameObject object1 = Instantiate(examplePrefab, topLeft, Quaternion.identity, this.transform);
            GameObject object2 = Instantiate(examplePrefab, topRight, Quaternion.identity, this.transform);
            GameObject door2 = Instantiate(doorTo2, doorLeft, Quaternion.identity, this.transform);
            
            // things that shouldn't be done when starting the game
            if(!startPosition) {
                PlayerController.instance.transform.position = new Vector3(-17.0f, PlayerController.instance.rb.position.y, 0.0f);
                floor.color = floorBlue;
            }

            startPosition = false;
            room1 = true;
        }
        
        // spawns room 2 if roomNumber is 2
        else if (roomNumber == 2 && room2 == false) {
            GameObject object1 = Instantiate(dog, bottomMiddle, Quaternion.identity, this.transform);
            GameObject object2 = Instantiate(examplePrefab, bottomLeft, Quaternion.identity, this.transform);
            GameObject door1 = Instantiate(doorTo1, doorRight, Quaternion.identity, this.transform);
            floor.color = floorGreen;
            PlayerController.instance.transform.position = new Vector3(9.0f, PlayerController.instance.rb.position.y, 0.0f);
            room2 = true;
        }

        // record roomNumber of previous frame
        // used to check for changes in room number
        roomNumberPrevFrame = roomNumber;
    }
}
