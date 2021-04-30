using UnityEngine;

public class Translator : MonoBehaviour
{
    public static Translator instanse;

    public bool isEng = true;

    public Languages rus;
    public Languages eng;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instanse = this;
    }
}
