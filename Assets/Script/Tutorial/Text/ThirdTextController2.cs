using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThirdTextController : MonoBehaviour
{

    string[] sentences; // 文章を格納する
    [SerializeField] TextMeshProUGUI uiText;   // uiTextへの参照
    [SerializeField] TutorialPlayer player;
    [SerializeField] Yadotyumuri yadotyumuri;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;   // 1文字の表示にかける時間

    public int currentSentenceNum { get; private set; } = 0; //現在表示している文章番号
    private string currentSentence = string.Empty;  // 現在の文字列
    private float timeUntilDisplay = 0;     // 表示にかかる時間
    private float timeBeganDisplay = 1;         // 文字列の表示を開始した時間
    private int lastUpdateCharCount = -1;       // 表示中の文字数

    public bool finished;
    void Start()
    {
        sentences = new string[]{
            "ナイスガード！",
            "SPACEキーを押している間しか　ガードできないから注意してネ！",
            "このゲージを見て！",
            "SPACEキーを押していると　　　このゲージが上下するヨ！",
            "SPACEキーを離すとその時の　　ゲージの量だけジャンプするヨ！",
            "ジャンプ中に再度キーを押すと　二段ジャンプできるヨ！",
            "百聞は一見に如かずだね！　　　さっそく実践してみよう！"

        };
        SetNextSentence();
    }


    public void TextUpdate()
    {
        // 文章の表示完了 / 未完了
        if (IsDisplayComplete())
        {
            //最後の文章ではない & ボタンが押された
            if (currentSentenceNum == 5)
            {
                if (yadotyumuri.isDoubleJump)
                {
                    StartCoroutine(WaitNextSentence());
                }
            }
            if (currentSentenceNum < sentences.Length && Input.GetKeyDown(KeyCode.Space))
            {
                SetNextSentence();
            }
            else if (currentSentenceNum >= sentences.Length)
            {
                finished = true;
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                timeUntilDisplay = 0;
            }
        }

        //表示される文字数を計算
        int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
        //表示される文字数が表示している文字数と違う
        if (displayCharCount != lastUpdateCharCount)
        {
            uiText.text = currentSentence.Substring(0, displayCharCount);
            //表示している文字数の更新
            lastUpdateCharCount = displayCharCount;
        }
    }

    private IEnumerator WaitNextSentence()
    {
        yield return new WaitUntil(() => player.isGround);

        SetNextSentence();
    }

    // 次の文章をセットする
    void SetNextSentence()
    {
        currentSentence = sentences[currentSentenceNum];
        timeUntilDisplay = currentSentence.Length * intervalForCharDisplay;
        timeBeganDisplay = Time.time;
        currentSentenceNum++;
        lastUpdateCharCount = 0;
    }

    bool IsDisplayComplete()
    {
        return Time.time > timeBeganDisplay + timeUntilDisplay; //※2
    }
}
