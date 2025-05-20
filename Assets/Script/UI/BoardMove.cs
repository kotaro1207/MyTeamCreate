using UnityEngine;

public class BoardMove : MonoBehaviour
{
    public RectTransform[] targets;          // �ړ���������������Image
    public Vector2 targetPosition = new Vector2(0f, 0f); // �����ʒu�iCanvas�̃T�C�Y�ɉ����āj
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


