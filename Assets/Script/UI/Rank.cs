using UnityEngine;
using UnityEngine.UI;

public class ScoreImageDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private Image image;
    private ScoreTime scoreTime;

    void Start()
    {
        image = GetComponent<Image>();
        scoreTime = GameObject.Find("ScoreManager").GetComponent<ScoreTime>();
    }

    private void Update()
    {
        //int scoreInt = Mathf.FloorToInt(scoreTime.score); // float → int
        UpdateImageByScore(GetImageIndexByScore(scoreTime.score));
    }

    int GetImageIndexByScore(float score)
    {
        // スコア帯に応じて表示する画像を選ぶ（調整可能）
        if (score >= 8000) return 0;
        else if (score >= 6000) return 1;
        else if (score >= 4000) return 2;
        else if (score > 3000) return 3;
        else if (score > 2000) return 4;
        else if (score > 1000) return 5;
        else return 5;
    }
    void UpdateImageByScore(int score)
    {
        image.sprite = sprites[score];
    }

}
