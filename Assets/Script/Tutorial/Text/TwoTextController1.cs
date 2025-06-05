using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TwoTextController : MonoBehaviour
{
    [SerializeField] private StopCollision stop;

    string[] sentences; // 文章を格納する
    [SerializeField] TextMeshProUGUI uiText;   // uiTextへの参照
    [SerializeField, Header("sound")] private AudioClip sound;
    private new AudioSource audio;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;   // 1文字の表示にかける時間

    public int currentSentenceNum = 0; //現在表示している文章番号
    private string currentSentence = string.Empty;  // 現在の文字列
    private float timeUntilDisplay = 0;     // 表示にかかる時間
    private float timeBeganDisplay = 1;         // 文字列の表示を開始した時間
    private int lastUpdateCharCount = -1;       // 表示中の文字数

    public bool finished;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        sentences = new string[]{
            "奥にいる敵のもとに行くとゴールだヨ！",
            "あっ！",
            "SPACEキーを押してガードしてみて！",
        };
        SetNextSentence();
    }

    private void Update()
    {

    }

    public void ManualNext()
    {
        SetNextSentence();
    }

    public void TextUpdate(/*bool _IsPush2*/)
    {
        // 文章の表示完了 / 未完了
        if (IsDisplayComplete())
        {
            //最後の文章ではない & ボタンが押された
            if (currentSentenceNum < sentences.Length && Input.GetKeyDown(KeyCode.Space) && currentSentenceNum != 2)
            {
                SetNextSentence();
            }
            else if (currentSentenceNum >= sentences.Length)
            {
                finished = true;
                //currentSentenceNum = 0;
            }
            else if(Input.GetKeyDown(KeyCode.Space) && currentSentenceNum == 2)
            {
                if(stop.isTouch)
                {
                    SetNextSentence();
                    Debug.Log("bbbb");
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                timeUntilDisplay = 0; //※1
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
            audio.PlayOneShot(sound);
        }
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
