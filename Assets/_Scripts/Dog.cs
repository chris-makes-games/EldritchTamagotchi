using UnityEngine;

public class Dog : MonoBehaviour
{   
    public static Dog instance;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite dog0, dog1, dog2;
    private int animationFrameCounter = 0;

    // death related variables
    public bool dead = false;
    private bool newDead = true;
    [SerializeField] private Collider2D dogWall;

    void Awake()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            animationFrameCounter++;
            if (animationFrameCounter == 18)
            {
                if (sr.sprite == dog0) sr.sprite = dog1;
                else if (sr.sprite == dog1) sr.sprite = dog0;
                animationFrameCounter = 0;
            }
            if (animationFrameCounter >= 19) animationFrameCounter = 0;
        }
        else if (newDead && dead)
        {
            sr.sprite = dog2;
            dogWall.enabled = false;
            transform.GetChild(1).gameObject.SetActive(false);
            newDead = false;
        }
    }
}
