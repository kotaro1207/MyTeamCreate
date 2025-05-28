using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdTutorialMain : MonoBehaviour
{
    [SerializeField]private ThirdTextController textController;
    public bool IsTextPush { get; private set; } = false;
    void Update()
    {
        textController.TextUpdate();
        IsTextPush = false;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            IsTextPush = true;
        }
    }
}
