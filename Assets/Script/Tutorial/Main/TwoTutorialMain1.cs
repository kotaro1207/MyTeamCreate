using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoTutorialMain : MonoBehaviour
{
    [SerializeField]private TwoTextController textController;
    public bool IsTextPush { get; private set; } = false;
    void Update()
    {
        if (textController.finished)
        {
            //textController.finished = false;
            return;
        }
        textController.TextUpdate();
        IsTextPush = false;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            IsTextPush = true;
        }
    }
}
