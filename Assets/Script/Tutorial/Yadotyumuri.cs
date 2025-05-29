using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Yadotyumuri : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject Triangle;
    [SerializeField] private GameObject MessageBox;
    [SerializeField] private GameObject textMesh;
    [SerializeField] private OneTextController text1;
    [SerializeField] private TwoTextController text2;
    [SerializeField] private ThirdTextController text3;
    [SerializeField] private OneTutorialMain main1;
    [SerializeField] private TwoTutorialMain main2;
    [SerializeField] private ThirdTutorialMain main3;
    [SerializeField] private Enemy enemy;
    [SerializeField] private TutorialCameraMove cameraMove;
    [SerializeField] private Image Gray;
    [SerializeField] private GameObject panel;
    private int count = 0;

    bool bullet = false;
    public bool isBullet = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(CameraInMove());
    }
    private void Update()
    {
        if (text1.finished)
        {
            animator.SetBool("isTalk", false);
            text1.enabled = false;
            main1.enabled = false;

            if (Input.GetKeyDown(KeyCode.Space) && count == 0)
            {
                Triangle.GetComponent<Image>().enabled = false;
                Triangle.GetComponent<Animator>().enabled = false;
                count++;
                StartCoroutine(secondsAnimation());
            }
        }
        if (text2.currentSentenceNum == 2 && !bullet)
        {
            panel.GetComponent<Animator>().SetBool("ON", false);

            StartCoroutine(BulletFire());
        }
        if (text2.finished)
        {
            animator.SetBool("isTalk", false);
            StartCoroutine(ThierdAnimation());
        }
    }
    private IEnumerator CameraInMove()
    {
        yield return new WaitForSeconds(1f);
        Gray.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        animator.enabled = true;
        text1.enabled = true;
        main1.enabled = true;
        MessageBox.SetActive(true);
        MessageBox.GetComponent<Animator>().enabled = true;
        textMesh.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        Triangle.GetComponent<Image>().enabled = true;
        Triangle.GetComponent<Animator>().enabled = true;
    }

    private IEnumerator secondsAnimation()
    {
        Gray.GetComponent<Animator>().SetBool("ON", false);
        textMesh.SetActive(false);
        Triangle.GetComponent<Image>().enabled = false;
        Triangle.GetComponent<Animator>().enabled = false;
        MessageBox.GetComponent<Animator>().SetBool("ON", false);
        yield return new WaitForSeconds(0.5f);
        cameraMove.CameraGoalFollow();
        yield return new WaitUntil(() => cameraMove.isFinished == true);
        //Gray.GetComponent<Animator>().SetBool("ON", true);
        animator.SetBool("isTalk", true);
        MessageBox.GetComponent<Animator>().SetBool("ON", true);
        yield return new WaitForSeconds(0.2f);
        textMesh.SetActive(true);
        textMesh.GetComponent<TextMeshProUGUI>().text = " ";
        main2.enabled = true;
        text2.enabled = true;
        panel.GetComponent<Image>().enabled = true;
        panel.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        Triangle.GetComponent<Image>().enabled = true;
        Triangle.GetComponent<Animator>().enabled = true;
    }

    private IEnumerator BulletFire()
    {
        bullet = true;
        yield return new WaitForSeconds(0.5f);
        enemy.ManualAtack();
    }

    private IEnumerator ThierdAnimation()
    {

        yield return new WaitForSeconds(1f);
        MessageBox.GetComponent<Animator>().SetBool("ON", false);
        textMesh.SetActive(false);
        main2.enabled = false;
        text2.enabled = false;
        //yield return new WaitForSeconds(0.25f);
        Triangle.GetComponent<Image>().enabled = false;
        Triangle.GetComponent<Animator>().enabled = false;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));


    }
}
