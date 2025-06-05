using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ENDButtonScript : MonoBehaviour
{
    SpriteRenderer sprite;
    private float time, changeSpeed;
    private bool transparent, pressed;

    [SerializeField]
    private Image image;

    [SerializeField] private GameObject FadeOut;

    public bool isUI;

    // フェード制御用
    public float fadeStartTime = 5f; // フェード開始までの秒数
    public float fadeOutDuration = 1f; // フェードアウトにかける秒数

    private float elapsedTime = 0f;
    private bool fadeStarted = false;

    private void Awake()
    {
        transparent = true;
        pressed = false;
        sprite = GetComponent<SpriteRenderer>();

        // 最初は透明にしておく
        if (!isUI)
        {
            if (sprite != null)
            {
                Color c = sprite.color;
                sprite.color = new Color(c.r, c.g, c.b, 0f);
            }
        }
        else if (image != null)
        {
            Color c = image.color;
            image.color = new Color(c.r, c.g, c.b, 0f);
        }
    }


    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (fadeStartTime <= elapsedTime)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
             LoadScene();
            }
        }
        else
        {}

        if (!pressed && !fadeStarted && elapsedTime >= fadeStartTime)
        {
            fadeStarted = true;
            StartCoroutine(FadeIn());
        }

        // AlphaChangeは無効化またはisUIチェック（フェードインに任せる）
        if (!pressed && !fadeStarted && !isUI)
        {
            AlphaChange();
        }
    }

    private void AlphaChange()
    {
        changeSpeed = Time.deltaTime * 0.7f;

        if (time < 0)
        {
            transparent = true;
        }
        if (time > 0.5f)
        {
            transparent = false;
        }

        if (transparent)
        {
            time += Time.deltaTime;
            sprite.color = sprite.color - new Color(0, 0, 0, changeSpeed);
        }
        else
        {
            time -= Time.deltaTime;
            sprite.color = sprite.color + new Color(0, 0, 0, changeSpeed);
        }
    }

    private void LoadScene()
    {
            pressed = true;

            if (!isUI)
            {
                sprite.color = sprite.color + new Color(0, 0, 0, 1);
            }
            else if (image != null)
            {
                image.color = image.color + new Color(0, 0, 0, 1);
            }

            StartCoroutine(Confirmed());
        
    }

    private IEnumerator Confirmed()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 squishedScale = originalScale * 0.85f;
        float squishDuration = 0.05f;
        float restoreDuration = 0.15f;

        float elapsed = 0f;
        while (elapsed < squishDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsed / squishDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = squishedScale;

        yield return new WaitForSeconds(0.025f);

        elapsed = 0f;
        while (elapsed < restoreDuration)
        {
            transform.localScale = Vector3.Lerp(squishedScale, originalScale, elapsed / restoreDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        yield return new WaitForSeconds(0.5f);

        if (image != null)
        {
            FadeOut.SetActive(true);
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        float duration = fadeOutDuration; // フェード時間は既存と共通

        if (!isUI)
        {
            Color c = sprite.color;
            sprite.color = new Color(c.r, c.g, c.b, 0f); // 初期透明

            while (elapsed < duration)
            {
                float alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
                sprite.color = new Color(c.r, c.g, c.b, alpha);
                elapsed += Time.deltaTime;
                yield return null;
            }

            sprite.color = new Color(c.r, c.g, c.b, 1f); // 最終不透明
        }
        else if (image != null)
        {
            Color c = image.color;
            image.color = new Color(c.r, c.g, c.b, 0f); // 初期透明

            while (elapsed < duration)
            {
                float alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
                image.color = new Color(c.r, c.g, c.b, alpha);
                elapsed += Time.deltaTime;
                yield return null;
            }

            image.color = new Color(c.r, c.g, c.b, 1f); // 最終不透明
        }
    }

}
