using System.Collections;
using UnityEngine;

public class BoardMove : MonoBehaviour
{
    public RectTransform[] targets;          // 移動させたい複数のImage
    public Vector2 targetPosition = new Vector2(0f, 0f); // 中央位置（Canvasのサイズに応じて）
    public float speed = 200f;
    [SerializeField, Header("ボタン")] private GameObject Button;

    private bool Pushed = false;
    private bool end = false;

    void Update()
    {
        bool allReached = true;

        foreach (RectTransform rect in targets)
        {
            if (rect != null)
            {
                rect.anchoredPosition = Vector2.MoveTowards(
                    rect.anchoredPosition,
                    targetPosition,
                    speed * Time.deltaTime
                );
            }

            if (Vector2.Distance(rect.anchoredPosition, targetPosition) > 0.1f)
            {
                allReached = false;
            }
        }

        if(allReached && !end)
        {
            Pushed = true; 
            end = true;
            StartCoroutine(LateEnabledChange());
        }
    }
    private IEnumerator LateEnabledChange()
    {
        yield return new WaitForSeconds(1f); 
        Button.SetActive(true);
        HPManager.Instance.Hp = 3;
    }
}


