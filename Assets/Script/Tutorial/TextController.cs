using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextController : MonoBehaviour
{

    string[] sentences; // ���͂��i�[����
    [SerializeField] TextMeshProUGUI uiText;   // uiText�ւ̎Q��

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;   // 1�����̕\���ɂ����鎞��

    private int currentSentenceNum = 0; //���ݕ\�����Ă��镶�͔ԍ�
    private string currentSentence = string.Empty;  // ���݂̕�����
    private float timeUntilDisplay = 0;     // �\���ɂ����鎞��
    private float timeBeganDisplay = 1;         // ������̕\�����J�n��������
    private int lastUpdateCharCount = -1;       // �\�����̕�����

    public bool finished;
    void Start()
    {
        sentences = new string[]{
            "�₠",
            "������`���[�g���A�����n�߂�l",
        };
        SetNextSentence();
    }


    public void TextUpdate(bool _IsPush)
    {
        // ���͂̕\������ / ������
        if (IsDisplayComplete())
        {
            //�Ō�̕��͂ł͂Ȃ� & �{�^���������ꂽ
            if (currentSentenceNum < sentences.Length && _IsPush)
            {
                SetNextSentence();
            }
            else if (currentSentenceNum >= sentences.Length)
            {
                finished = true;
                currentSentenceNum = 0;
            }
        }
        else
        {
            if (_IsPush)
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
