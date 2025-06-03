using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class ENDButtonScript : MonoBehaviour
{
    SpriteRenderer sprite;
    private float time, changeSpeed;
    private bool transparent, pressed;

    [SerializeField]
    private GameObject image;

    public bool isUI;

    // �t�F�[�h����p
    public float fadeStartTime = 5f; // �t�F�[�h�J�n�܂ł̕b��
    public float fadeOutDuration = 1f; // �t�F�[�h�A�E�g�ɂ�����b��

    private bool fadeStarted = false;

    private void Awake()
    {
        transparent = true;
        pressed = false;
        sprite = GetComponent<SpriteRenderer>();

        // �ŏ��͓����ɂ��Ă���
        if (!isUI)
        {
            if (sprite != null)
            {
                Color c = sprite.color;
                sprite.color = new Color(c.r, c.g, c.b, 0f);
            }
        }
        else if (image != null)
        {
            Color c = image.GetComponent<Image>().color;
            image.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0f);
        }
    }


    void Update()
    {
        //elapsedTime += Time.deltaTime;

        // AlphaChange�͖������܂���isUI�`�F�b�N�i�t�F�[�h�C���ɔC����j
        if (!pressed && !fadeStarted && !isUI)
        {
            AlphaChange();
        }
    }


    private void AlphaChange()
    {
        changeSpeed = Time.deltaTime * 0.7f;

        if (time < 0)
        {
            transparent = true;
        }
        if (time > 0.5f)
        {
            transparent = false;
        }

        if (transparent)
        {
            time += Time.deltaTime;
            sprite.color = sprite.color - new Color(0, 0, 0, changeSpeed);
        }
        else
        {
            time -= Time.deltaTime;
            sprite.color = sprite.color + new Color(0, 0, 0, changeSpeed);
        }
    }

    private void LoadScene()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pressed)
        {
            Debug.Log("�^�C�g���V�[���Ɉڍs���܂�");
            image.GetComponent<Image>().enabled = true;
            pressed = true;

            if (!isUI)
            {
                sprite.color = sprite.color + new Color(0, 0, 0, 1);
            }
            else if (image != null)
            {
                image.GetComponent<Image>().color = image.GetComponent<Image>().color + new Color(0, 0, 0, 1);
            }
            StartCoroutine(Confirmed());
            StartCoroutine(FadeIn());

        }
    }

    private IEnumerator Confirmed()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 squishedScale = originalScale * 0.85f;
        float squishDuration = 0.05f;
        float restoreDuration = 0.15f;

        float elapsed = 0f;
        while (elapsed < squishDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsed / squishDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = squishedScale;

        yield return new WaitForSeconds(0.025f);

        elapsed = 0f;
        while (elapsed < restoreDuration)
        {
            transform.localScale = Vector3.Lerp(squishedScale, originalScale, elapsed / restoreDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        yield return new WaitForSeconds(0.5f);

        if (image != null)
        {
            image.GetComponent<Animator>().enabled = true;
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        float duration = fadeOutDuration; // �t�F�[�h���Ԃ͊����Ƌ���

        if (!isUI)
        {
            Color c = sprite.color;
            sprite.color = new Color(c.r, c.g, c.b, 0f); // ��������

            while (elapsed < duration)
            {
                float alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
                sprite.color = new Color(c.r, c.g, c.b, alpha);
                elapsed += Time.deltaTime;
                yield return null;
            }

            sprite.color = new Color(c.r, c.g, c.b, 1f); // �ŏI�s����
        }
        else if (image != null)
        {
            Color c = image.GetComponent<Image>().color;
            image.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0f); // ��������

            while (elapsed < duration)
            {
                float alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
                image.GetComponent<Image>().color = new Color(c.r, c.g, c.b, alpha);
                elapsed += Time.deltaTime;
                yield return null;
            }

            image.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1f); // �ŏI�s����

        }
    }

}
