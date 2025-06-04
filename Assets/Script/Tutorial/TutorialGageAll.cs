using UnityEngine;
using System.Collections;

public class TutorialGageAll : MonoBehaviour
{
    public GameObject[] targetObjects; // 表示・非表示を切り替える複数のオブジェクト
    [SerializeField] private TutorialPlayer player;
    [SerializeField] private Gage guage;

    private bool isCoroutineRunning = false;

    void Update()
    {
        if (player.ManualShell && player.isGround)
        {
            guage.enabled = true;
            // スペースキーを押している間は常に表示
            ShowTargets();
            if (isCoroutineRunning)
            {
                StopAllCoroutines();
                isCoroutineRunning = false;
            }
        }
        else if (!isCoroutineRunning)
        {
            // キーを離した瞬間にコルーチンを開始
            StartCoroutine(HideAfterDelay(0));
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
