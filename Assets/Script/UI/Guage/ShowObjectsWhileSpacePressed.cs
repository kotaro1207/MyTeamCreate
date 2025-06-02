using UnityEngine;

public class ShowObjectsWhileSpacePressed : MonoBehaviour
{
    public GameObject[] targetObjects; // 表示・非表示を切り替える複数のオブジェクト

    void Update()
    {
        bool shouldShow = Input.GetKey(KeyCode.Space);

        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
            {
                obj.SetActive(shouldShow);
            }
        }
    }
}