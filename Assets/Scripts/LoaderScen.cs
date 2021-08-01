using UnityEngine;
using UnityEngine.SceneManagement;
public class LoaderScen : MonoBehaviour
{
    public static LoaderScen instance;

    private static bool shouldPlayOpeningAnimation = false;

    private AsyncOperation loadingSceneOperation;
    private Animator animator;

    private void Awake()
    {
        instance = this;
       // DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (shouldPlayOpeningAnimation)
        {
            animator.SetTrigger("StartLevel");
            shouldPlayOpeningAnimation = false;
        }
    }

    

    public void TransitToScene(int indexScene)
    {
        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(indexScene);
        instance.loadingSceneOperation.allowSceneActivation = false;
        animator.SetTrigger("StartLoad");
    }

    public void OnAnimationOwer()
    {
        shouldPlayOpeningAnimation = true;
        loadingSceneOperation.allowSceneActivation = true;
    }


}
