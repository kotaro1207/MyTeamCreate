using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField, Header("ƒV[ƒ“–¼‚ğ“ü—Í")]
    private string sceneName;

    public bool isButtonDown = false;

    private void Update()
    {
        if(isButtonDown && Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene();
        }
    }


    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
