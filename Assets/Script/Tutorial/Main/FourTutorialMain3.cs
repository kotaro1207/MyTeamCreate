using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FourTutorialMain : MonoBehaviour
{
    [SerializeField]private FourTextController textController;
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
