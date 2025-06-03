using TMPro;
using UnityEngine;
public class ResultTime : MonoBehaviour
{
    private ScoreTime time;
    public TMP_Text timeText;
    void Start()
    {
        // ��A�N�e�B�u���܂߂đS�I�u�W�F�N�g����T����
        ScoreTime[] alltimes = Resources.FindObjectsOfTypeAll<ScoreTime>();
        if (alltimes.Length > 0)
        {
            time = alltimes[0];
        }
        else
        {
            Debug.LogWarning("ScoreTime ��������܂���I");
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