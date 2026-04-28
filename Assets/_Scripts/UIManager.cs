using UnityEngine;

public class UIManager : MonoBehaviour
{
    //this is a singleton object, this line makes the instance static
    private static UIManager _instance;
    public static UIManager instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
