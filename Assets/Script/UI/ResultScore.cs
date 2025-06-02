using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ResultScore : MonoBehaviour
{
    private ScoreTime score;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // ��A�N�e�B�u���܂߂đS�I�u�W�F�N�g����T����
        ScoreTime[] allScores = Resources.FindObjectsOfTypeAll<ScoreTime>();
        if (allScores.Length > 0)
        {
            score = allScores[0];
        }
        else
        {
            Debug.LogWarning("ScoreTime ��������܂���I");
        }
    }
    void Update()
    {
        if (score != null && scoreText != null)
        {
            scoreText.text = score.score.ToString("F0");
        }
    }
}
