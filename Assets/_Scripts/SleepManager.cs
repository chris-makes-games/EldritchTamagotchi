using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

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

    //singleton instance
    private static SleepManager instance;

    private void Awake()
    {
        instance = this;//ensure singleton
    }

    public static SleepManager GetInstance()
    {
        return instance;//ensure singleton
    }

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
        Color tmp = mainRenderer.color;
        tmp.a = 0;
        mainRenderer.color = tmp;
        StartCoroutine(FadeAway());
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
        yield return StartCoroutine(FadeIn(mainRenderer));
    }

    public IEnumerator FadeAway()
    {
        yield return StartCoroutine(FadeOut(mainRenderer));
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
        yield return null;
        
    }

    private IEnumerator FadeIn(SpriteRenderer sr)
    {
        float alphaVal = sr.color.a;
        Color tmp = sr.color;

        while (sr.color.a < 1)
        {
            alphaVal += 0.07f;
            tmp.a = alphaVal;
            sr.color = tmp;

            yield return new WaitForSeconds(0.10f); // update interval
        }
        yield return null;

    }
}
