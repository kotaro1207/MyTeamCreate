using TMPro;
using UnityEngine;
public class ResultTime : MonoBehaviour
{
    private ScoreTime time;
    public TMP_Text timeText;
    void Start()
    {
        // 非アクティブも含めて全オブジェクトから探す例
        ScoreTime[] alltimes = Resources.FindObjectsOfTypeAll<ScoreTime>();
        if (alltimes.Length > 0)
        {
            time = alltimes[0];
        }
        else
        {
            Debug.LogWarning("ScoreTime が見つかりません！");
        }
    }

    void Update()
    {
        if (time != null && timeText != null)
        {
            timeText.text = " " + time.time.ToString("F0");
        }
    }
}