using UnityEngine;

public class Dog : MonoBehaviour
{
    // sprite/animation stuff
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite dog0, dog1;
    public int animationFrameCounter = 0;
    
    void FixedUpdate()
    {
        // dog animation!
        animationFrameCounter++;

        if (animationFrameCounter == 18)
        {
            if (sr.sprite == dog0) sr.sprite = dog1;
            else if (sr.sprite == dog1) sr.sprite = dog0;
            animationFrameCounter = 0;
        }
        if (animationFrameCounter >= 19) animationFrameCounter = 0;
    }
}
