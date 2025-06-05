using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TwoTextController : MonoBehaviour
{
    [SerializeField] private StopCollision stop;

    string[] sentences; // ���͂��i�[����
    [SerializeField] TextMeshProUGUI uiText;   // uiText�ւ̎Q��
    [SerializeField, Header("sound")] private AudioClip sound;
    private new AudioSource audio;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;   // 1�����̕\���ɂ����鎞��

    public int currentSentenceNum = 0; //���ݕ\�����Ă��镶�͔ԍ�
    private string currentSentence = string.Empty;  // ���݂̕�����
    private float timeUntilDisplay = 0;     // �\���ɂ����鎞��
    private float timeBeganDisplay = 1;         // ������̕\�����J�n��������
    private int lastUpdateCharCount = -1;       // �\�����̕�����

    public bool finished;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        sentences = new string[]{
            "���ɂ���G�̂��Ƃɍs���ƃS�[�������I",
            "�����I",
            "SPACE�L�[�������ăK�[�h���Ă݂āI",
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
        // ���͂̕\������ / ������
        if (IsDisplayComplete())
        {
            //�Ō�̕��͂ł͂Ȃ� & �{�^���������ꂽ
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
                timeUntilDisplay = 0; //��1
            }
        }

        //�\������镶�������v�Z
        int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
        //�\������镶�������\�����Ă��镶�����ƈႤ
        if (displayCharCount != lastUpdateCharCount)
        {
            uiText.text = currentSentence.Substring(0, displayCharCount);
            //�\�����Ă��镶�����̍X�V
            lastUpdateCharCount = displayCharCount;
            audio.PlayOneShot(sound);
        }
    }

    // ���̕��͂��Z�b�g����
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
        return Time.time > timeBeganDisplay + timeUntilDisplay; //��2
    }
}
