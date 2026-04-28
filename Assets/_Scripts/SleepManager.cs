using System.Collections;
using UnityEngine;

public class SleepManager : MonoBehaviour
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
    [SerializeField] private float fadeValue;

    private void OnEnable()
    {
        QuestManager.SceneLoadEvent += SceneLoaded;
    }

    private void OnDisable()
    {
        QuestManager.SceneLoadEvent -= SceneLoaded;
    }

    private void SceneLoaded(QuestManager sceneLoad)
    {
        //removing fade on load
        //StartCoroutine(FadeAway());
    }


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

    public IEnumerator FadeToBlack()
    {
        Debug.Log("fading to black");
        yield return StartCoroutine(FadeIn(mainRenderer));
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator FadeAway()
    {
        Debug.Log("fading away");
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOut(mainRenderer));

    }

    private IEnumerator FadeOut(SpriteRenderer sr)
    {
        fadeValue = sr.color.a;
        Color tmp = sr.color;

        while (sr.color.a > 0)
        {
            fadeValue -= 0.07f;
            tmp.a = fadeValue;
            sr.color = tmp;

            yield return new WaitForSeconds(0.10f); // update interval
        }
        
    }

    private IEnumerator FadeIn(SpriteRenderer sr)
    {
        fadeValue = sr.color.a;
        Color tmp = sr.color;

        while (sr.color.a < 1)
        {
            fadeValue += 0.07f;
            tmp.a = fadeValue;
            sr.color = tmp;

            yield return new WaitForSeconds(0.10f); // update interval
        }

    }
}
