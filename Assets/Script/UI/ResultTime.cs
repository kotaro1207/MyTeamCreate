using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ResultTime : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // スコアを表示するTextMeshProUGUI

    void Start()
    {
        float Time = Score.elapsedTime;

        // スコアを表示
        scoreText.text = Mathf.FloorToInt(Time).ToString();
    }
}