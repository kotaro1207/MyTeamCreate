using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static float score = 0f;               // スコアを他スクリプトからも参照できるように
    public static float elapsedTime = 0f;         // ? 時間計測用のstatic変数を追加！

    public float initialScore = 10000f;
    public float timeOffset = 1f;
    public string PlayerTag = "Player";
    public TextMeshProUGUI scoreText;

    private bool isPlayerAlive = true;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("scoreTextが設定されていません。Inspectorで設定してください。");
        }
    }

    void Update()
    {
        if (GameObject.FindWithTag(PlayerTag) == null)
        {
            isPlayerAlive = false;
        }

        if (isPlayerAlive)
        {
            elapsedTime += Time.deltaTime; // ? 時間を毎フレーム加算（秒）

            score = initialScore / (elapsedTime + timeOffset);

            if (scoreText != null)
            {
                scoreText.text = Mathf.FloorToInt(score).ToString();
            }
        }
        else
        {
            if (scoreText != null)
            {
                scoreText.text = Mathf.FloorToInt(score).ToString();
            }
        }
    }
}
