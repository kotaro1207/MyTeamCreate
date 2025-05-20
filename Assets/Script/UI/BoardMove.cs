using UnityEngine;

public class BoardMove : MonoBehaviour
{
    public RectTransform[] targets;          // 移動させたい複数のImage
    public Vector2 targetPosition = new Vector2(0f, 0f); // 中央位置（Canvasのサイズに応じて）
    public float speed = 200f;
    public SceneChange SceneChange;

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

        if(allReached)
        {
            SceneChange.enabled = true;
        }
        else
        {
            SceneChange.enabled = false;
        }
    }
}


