using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneTutorialMain : MonoBehaviour
{
    [SerializeField]private OneTextController textController;
    public bool IsTextPush { get; private set; } = false;
    void Update()
    {
        if (textController.finished)
        {
            //textController.finished = false;
            return;
        }
        textController.TextUpdate(IsTextPush);
        IsTextPush = false;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            IsTextPush = true;
        }
    }
}
