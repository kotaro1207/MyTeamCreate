using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ResultScore : MonoBehaviour
{
    private ScoreTime score;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // 非アクティブも含めて全オブジェクトから探す例
        ScoreTime[] allScores = Resources.FindObjectsOfTypeAll<ScoreTime>();
        if (allScores.Length > 0)
        {
            score = allScores[0];
        }
        else
        {
            Debug.LogWarning("ScoreTime が見つかりません！");
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
