using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField, Header("ÉVÅ[ÉìñºÇì¸óÕ")]
    private string sceneName;

    public bool isButtonDown = false;

    private void Update()
    {
        if (isButtonDown && Input.GetKeyDown(KeyCode.Space))
        {
            HPManager.Instance.Hp = 3;
            LoadScene();
        }
    }


    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
