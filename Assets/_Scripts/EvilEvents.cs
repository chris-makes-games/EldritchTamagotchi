using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class EvilEvents : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    public static EvilEvents instance;
    [SerializeField] private TextMeshProUGUI endText;
    public bool ending = true;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private GameObject knife;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(EvilStart());
    }

    void Update()
    {
        if(ending) PlayerController.instance.canMove = false;
    }

    public void EndFade()
    {
        StartCoroutine(FadeIn(sr));
    }

    private IEnumerator EvilStart()
    {
        List<string> phrases = new List<string> {"oh", "that's no good", "why don't you just get rid of that dog", "here, take this"};
        
        StartCoroutine(questManager.ChainText(phrases));
        yield return new WaitForSeconds(16.5f);
        knife.SetActive(true);

        yield return null;
    }

    private IEnumerator FadeIn(SpriteRenderer sr)
    {
        yield return new WaitForSeconds(2.0f);

        float alphaVal = sr.color.a;
        Color tmp = sr.color;

        while (sr.color.a < 1)
        {
            alphaVal += 0.04f;
            tmp.a = alphaVal;
            sr.color = tmp;

            yield return new WaitForSeconds(0.10f); // update interval
        }

        yield return new WaitForSeconds(0.5f);

        // might do a fade-in for the text instead of this, too tired for now
        endText.enabled = true;

        yield return new WaitForSeconds(3.0f);

        Cursor.visible = true;
        Debug.Log("Returning to main menu...");
        SceneManager.LoadScene("StartMenu");

        yield return null;
    }
}
