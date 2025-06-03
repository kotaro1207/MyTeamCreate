
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    SpriteRenderer sprite;
    private float time, changeSpeed;
    private bool transparent, pressed;

    [SerializeField]
    private Image image;

    [SerializeField] AudioSource audioSource;
    [SerializeField, Header("Sound")]
    private AudioClip sound;

    public bool isUI;
    public bool StartAlphaOne = false;
    public bool AlphaChanged = true;

    private void Awake()
    {
        transparent = true;
        pressed = false;
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        LoadScene();
        if (StartAlphaOne) StartAlphaChange();

        if (!pressed && isUI && AlphaChanged) AlphaChange();
    }

    private void StartAlphaChange()
    {
        StartAlphaOne = false;

        if (sprite.color.a == 1) AlphaChanged = true;

        while (!AlphaChanged)
        {
            time -= Time.deltaTime;
            sprite.color = sprite.color + new Color(0, 0, 0, changeSpeed);//ìßñæìxDOWN
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

        if (transparent == true)
        {
            time += Time.deltaTime;
            sprite.color = sprite.color - new Color(0, 0, 0, changeSpeed); //ìßñæìxUP
        }
        else
        {
            time -= Time.deltaTime;
            sprite.color = sprite.color + new Color(0, 0, 0, changeSpeed);//ìßñæìxDOWN
        }
    }

    private void LoadScene()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pressed)
        {
            pressed = true;
            StartCoroutine(Confirmed());
            audioSource.PlayOneShot(sound);
            sprite.color = sprite.color + new Color(0, 0, 0, 1);
        }
    }

    private IEnumerator Confirmed()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 squishedScale = originalScale * 0.85f;
        float squishDuration = 0.05f;
        float restoreDuration = 0.15f;

        // èkÇﬁ
        float elapsed = 0f;
        while (elapsed < squishDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsed / squishDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = squishedScale;

        yield return new WaitForSeconds(0.025f);

        // ñﬂÇÈ
        elapsed = 0f;
        while (elapsed < restoreDuration)
        {
            transform.localScale = Vector3.Lerp(squishedScale, originalScale, elapsed / restoreDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        yield return new WaitForSeconds(0.5f);

        image.GetComponent<Animator>().enabled = true;

    }

}