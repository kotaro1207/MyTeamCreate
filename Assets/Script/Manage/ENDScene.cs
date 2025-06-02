using UnityEngine;
using UnityEngine.SceneManagement;

public class ENDScene : MonoBehaviour
{
    public float waittime = 10f;
    public float Changetime;
    public void LoadScene()
    {
        Changetime += Time.deltaTime;
        Debug.Log(Changetime);
        if (waittime <= Changetime)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}