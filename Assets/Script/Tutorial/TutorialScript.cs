using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [SerializeField, Header("Player Script")]
    private TutorialPlayer player;

    [SerializeField, Header("Enemy Script")]
    private Enemy enemy;

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
        PushCheck();
        SceneChange();

    }

    private void PushCheck()
    {
        if (isPush)
        {
            PushTime += Time.deltaTime;
            SkipButton.fillAmount += 1.0f / PushMax * Time.deltaTime;
        }
        else
        {
            PushTime = 0f;
            if (!SceneChange_) SkipButton.fillAmount = 0;
            else SkipButton.fillAmount = 1;
        }
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
