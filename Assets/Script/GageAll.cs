using UnityEngine;
using System.Collections;

public class GageAll : MonoBehaviour
{
    public GameObject[] targetObjects; // �\���E��\����؂�ւ��镡���̃I�u�W�F�N�g
    [SerializeField] private Player player;
    [SerializeField] private Gage guage;

    private bool isCoroutineRunning = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)&& player.isGround)
        {
            guage.enabled = true;
            // �X�y�[�X�L�[�������Ă���Ԃ͏�ɕ\��
            ShowTargets();
            if (isCoroutineRunning)
            {
                StopAllCoroutines();
                isCoroutineRunning = false;
            }
        }
        else if (!isCoroutineRunning)
        {
            // �L�[�𗣂����u�ԂɃR���[�`�����J�n
            StartCoroutine(HideAfterDelay(0.1f));
        }
    }

    private IEnumerator HideAfterDelay(float delay = 0f)
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(delay);
        guage.enabled = false;
        HideTargets();
        isCoroutineRunning = false;
    }

    private void ShowTargets()
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null) obj.SetActive(true);
        }
    }

    private void HideTargets()
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null) obj.SetActive(false);
        }
    }
}
