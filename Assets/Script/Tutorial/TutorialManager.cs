using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    public bool tutorialCompleted = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ƒQ[ƒ€‹N“®’†‚¸‚Á‚Æ•Û
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
