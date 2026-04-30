using System;
using UnityEngine;

public class Animateable : MonoBehaviour //moved animation stuff from Owen's dog into here, for any animation
{
    [SerializeField] private Sprite[] frames;
    [SerializeField] private int frameDelay = 18;
    SpriteRenderer sr;
    private int currentFrame = 0;
    private int animationFrameCounter = 0;
    private bool dead = false;

    //unity event: I am a wizard - Chris
    public static event Action<Animateable> ChangeSpriteEvent;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InkManager.EndEvent += EndEvent;
    }

    private void OnDisable()
    {
        InkManager.EndEvent -= EndEvent;
    }

    private void EndEvent(InkManager ink)
    {
        dead = true;
    }


    void FixedUpdate()
    {
        if (dead)
        {
            return;
        }

        animationFrameCounter++;
        if (animationFrameCounter == frameDelay)
        {
            if (currentFrame == frames.Length - 1)
            {
                currentFrame = 0; //wraps back around to beginning of array
            }
            else
            {
                currentFrame += 1; //goes to next sprite in array
            }
            sr.sprite = frames[currentFrame]; //sets new sprite

            //invokes the event
            //can be used by any other object listening to this event
            ChangeSpriteEvent?.Invoke(this);

        }
        //resets counter
        if (animationFrameCounter > frameDelay) animationFrameCounter = 0;
        
    }
}
