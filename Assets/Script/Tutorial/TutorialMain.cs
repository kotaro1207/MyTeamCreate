using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMain : MonoBehaviour
{
    [SerializeField]
    TextController textController;
    bool IsTextPush = false;
    void Update()
    {
        if (textController.finished)
        {
            textController.finished = false;
            return;
        }
        textController.TextUpdate(IsTextPush);
        IsTextPush = false;
    }
    public void PushText()
    {
        IsTextPush = true;
    }
}
