using System.Collections;
using UnityEngine;

public class PrologueScript : MonoBehaviour
{
    [SerializeField, Header("“±“üŠG")] private Sprite[] sprites;
    [SerializeField, Header("Fade Out")] private GameObject Fade;
    [SerializeField, Header("SpaceKeyText Script")] private SpaceKeyText text;
    private SpriteRenderer spriteRenderer;
    bool isStart = false;
    bool end = false;
    int frame = 0;
    public float time;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(LateMove());
        spriteRenderer.sprite = sprites[frame];
        Fade.SetActive(false);
    }

    private void Update()
    {
        if (isStart)
        {
            if (!end) time += Time.deltaTime;
            DownNext();
            TextEnabled();
        }


    }

    private void DownNext()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (frame < 3)
            {
                frame++;
                spriteRenderer.sprite = sprites[frame];
                time = 0;
            }
            else if (frame >= 3)
            {
                end = true;
                time = 0;
                Fade.SetActive(true);
            }
        }
    }

    private void TextEnabled()
    {
        if (time >= 8)
        {
            text.WakeAnimator();
        }
        else
        {
            text.SleepAnimator();
        }
    }

    private IEnumerator LateMove()
    {
        yield return new WaitForSeconds(1f);
        isStart = true;
    }

}
