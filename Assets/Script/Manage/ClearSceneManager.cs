using UnityEngine;

public class ClearSceneManager : MonoBehaviour
{
    [SerializeField]
    private SlightDarkenAfterDelay Picture;

    [SerializeField]
    private BoardMove BoardMove;

    [SerializeField]
    private SceneChange SceneChange;

    private bool isDownOne = false;

    float time;
    float duration = 1f;

    private void Start()
    {
        SceneChange.enabled = false;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (duration < time)
        {
           // isCheck();
        }
    }

    //private void isCheck()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space)&&!isDownOne&&!Picture.isEnd&&!BoardMove.isEnd)
    //    {
    //        Picture.End();
    //        BoardMove.End();
    //        isDownOne = true;
    //    }
    //    else if(Input.GetKeyDown(KeyCode.Space)&&isDownOne)
    //    {
    //        SceneChange.enabled = true;
    //    }
    //}
}
