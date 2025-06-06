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
            DontDestroyOnLoad(gameObject); // ゲーム起動中ずっと保持
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
