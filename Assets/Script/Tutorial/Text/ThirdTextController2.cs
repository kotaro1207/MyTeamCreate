using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThirdTextController : MonoBehaviour
{

    string[] sentences; // ���͂��i�[����
    [SerializeField] TextMeshProUGUI uiText;   // uiText�ւ̎Q��
    [SerializeField] TutorialPlayer player;
    [SerializeField] Yadotyumuri yadotyumuri;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;   // 1�����̕\���ɂ����鎞��

    public int currentSentenceNum { get; private set; } = 0; //���ݕ\�����Ă��镶�͔ԍ�
    private string currentSentence = string.Empty;  // ���݂̕�����
    private float timeUntilDisplay = 0;     // �\���ɂ����鎞��
    private float timeBeganDisplay = 1;         // ������̕\�����J�n��������
    private int lastUpdateCharCount = -1;       // �\�����̕�����

    public bool finished;
    void Start()
    {
        sentences = new string[]{
            "�i�C�X�K�[�h�I",
            "SPACE�L�[�������Ă���Ԃ����@�K�[�h�ł��Ȃ����璍�ӂ��ăl�I",
            "���̃Q�[�W�����āI",
            "SPACE�L�[�������Ă���Ɓ@�@�@���̃Q�[�W���㉺���郈�I",
            "SPACE�L�[�𗣂��Ƃ��̎��́@�@�Q�[�W�̗ʂ����W�����v���郈�I",
            "�W�����v���ɍēx�L�[�������Ɓ@��i�W�����v�ł��郈�I",
            "�S���͈ꌩ�ɔ@�������ˁI�@�@�@�����������H���Ă݂悤�I"

        };
        SetNextSentence();
    }


    public void TextUpdate()
    {
        // ���͂̕\������ / ������
        if (IsDisplayComplete())
        {
            //�Ō�̕��͂ł͂Ȃ� & �{�^���������ꂽ
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

    private IEnumerator WaitNextSentence()
    {
        yield return new WaitUntil(() => player.isGround);

        SetNextSentence();
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
