using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ResultScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // スコアを表示するTextMeshProUGUI

    void Start()
    {
        // ScoreManagerのstaticスコアを取得
        float score = Score.score;

        // スコアを表示
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
}
