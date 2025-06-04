using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [SerializeField, Header("Player Script")]
    private TutorialPlayer player;

    [SerializeField, Header("FadeOut Image")]
    private GameObject FadeOut;

    [SerializeField, Header("Skip Button")]
    private Image SkipButton;


    private float PushTime;
    private float PushMax = 4f;
    private bool SceneChange_ = false;
    public bool isPush => Input.GetKey(KeyCode.Space);


    private void Update()
    {
        SceneChange();
    }

    private void SceneChange()
    {
        if (PushMax <= PushTime && !SceneChange_)
        {
            FadeOut.SetActive(true);
            SceneChange_ = true;
        }
    }

    //private void 
}
