using UnityEngine;

public class ShowObjectsWhileSpacePressed : MonoBehaviour
{
    public GameObject[] targetObjects; // �\���E��\����؂�ւ��镡���̃I�u�W�F�N�g

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