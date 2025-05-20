using UnityEngine;
using System.Collections;

public class SlightDarkenAfterDelay : MonoBehaviour
{
    public float delaySeconds = 3f;
    public float darkenDuration = 2f;
    public float darknessFactor = 0.8f; // 1.0 = 元の色, 0.0 = 黒, 0.8 = 少し暗い

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color targetColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        // 少し暗くした色を計算（RGB値をdarknessFactor倍）
        targetColor = new Color(
            originalColor.r * darknessFactor,
            originalColor.g * darknessFactor,
            originalColor.b * darknessFactor,
            originalColor.a // 透明度はそのまま
        );

        StartCoroutine(DarkenAfterDelayCoroutine());
    }

    IEnumerator DarkenAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delaySeconds);

        float elapsed = 0f;
        while (elapsed < darkenDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / darkenDuration;

            spriteRenderer.color = Color.Lerp(originalColor, targetColor, t);
            yield return null;
        }

        // 念のため、最後に確定
        spriteRenderer.color = targetColor;
    }
}


