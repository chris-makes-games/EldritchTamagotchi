using UnityEngine;

public class Sink : MonoBehaviour
{
    [SerializeField] private GameObject sinkBlue;
    [SerializeField] private GameObject sinkBlack;
    private SpriteRenderer mainSR;

    private void Start()
    {
        mainSR = GetComponent<SpriteRenderer>();
        sinkBlue.SetActive(false);
        sinkBlack.SetActive(false);
    }

    private void OnEnable()
    {
        InkManager.SinkEvent += sinkChange;
    }

    private void OnDisable()
    {
        InkManager.SinkEvent -= sinkChange;
    }

    private void sinkChange(int status)
    {
        // 0 off
        // 1 black
        // 2 blue
        switch (status)
        {
            case 0:
                mainSR.enabled = true;
                sinkBlue.SetActive(false);
                sinkBlue.SetActive(false);
                break;

            case 1:
                mainSR.enabled = false;
                sinkBlue.SetActive(false);
                sinkBlack.SetActive(true);
                break;

            case 2:
                mainSR.enabled = false;
                sinkBlue.SetActive(true);
                sinkBlack.SetActive(false);
                break;
        }
    }
}
