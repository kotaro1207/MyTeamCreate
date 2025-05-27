using UnityEngine;
using UnityEngine.UI;

public class ButtonDownIMage : MonoBehaviour
{
    [SerializeField] private Sprite[] sprite;
    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            image.sprite = sprite[0];
        }
        else
        {
            image.sprite = sprite[1];
        }
    }

}
