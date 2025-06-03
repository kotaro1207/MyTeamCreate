using System.Collections;
using UnityEngine;

public class BoardMove : MonoBehaviour
{
    public RectTransform[] targets;          // �ړ���������������Image
    public Vector2 targetPosition = new Vector2(0f, 0f); // �����ʒu�iCanvas�̃T�C�Y�ɉ����āj
    public float speed = 200f;
    public SceneChange SceneChange;
    [SerializeField, Header("�{�^��")] private ENDButtonScript Button;

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
        SceneChange.enabled = true;
        Button.enabled = true;
    }
}


