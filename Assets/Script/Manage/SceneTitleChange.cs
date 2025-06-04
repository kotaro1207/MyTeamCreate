using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTitleChange : MonoBehaviour
{
    public bool isButtonDown = false;

    private void Update()
    {
        if (isButtonDown && Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene();
        }
    }
    public void LoadScene()
    {
        if (TutorialManager.Instance != null && TutorialManager.Instance.tutorialCompleted)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            SceneManager.LoadScene("PrologueScene");
        }
    }
}
