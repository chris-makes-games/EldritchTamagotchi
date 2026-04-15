using UnityEngine;
using System.Collections;

public class wakeUp : MonoBehaviour
{
    [SerializeField]
    private GameObject main;
    [SerializeField]
    private GameObject side1;
    [SerializeField]
    private GameObject side2;
    [SerializeField]
    private GameObject fakePlayer;

    private SpriteRenderer mainRenderer;
    private SpriteRenderer side1Renderer;
    private SpriteRenderer side2Renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainRenderer = main.GetComponent<SpriteRenderer>();
        side1Renderer = side1.GetComponent<SpriteRenderer>();
        side2Renderer = side2.GetComponent<SpriteRenderer>();
    }

    public void Awaken()
    {
        fakePlayer.SetActive(false);
        StartCoroutine(FadeOut(mainRenderer));
        StartCoroutine(FadeOut(side1Renderer));
        StartCoroutine(FadeOut(side2Renderer));
    }

    private IEnumerator FadeOut(SpriteRenderer sr)
    {
        float alphaVal = sr.color.a;
        Color tmp = sr.color;

        while (sr.color.a > 0)
        {
            alphaVal -= 0.07f;
            tmp.a = alphaVal;
            sr.color = tmp;

            yield return new WaitForSeconds(0.10f); // update interval
        }
        
    }
}
