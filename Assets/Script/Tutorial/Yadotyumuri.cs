using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Yadotyumuri : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject MessageBox;
    [SerializeField] private GameObject text;
    [SerializeField] private OneTextController text1;
    [SerializeField] private TwoTextController text2;
    [SerializeField] private OneTutorialMain main1;
    [SerializeField] private TwoTutorialMain main2;
    [SerializeField] private Enemy enemy;
    [SerializeField] private TutorialCameraMove cameraMove;
    [SerializeField] private Image Gray;
    private int count = 0;

    bool bullet = false;

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
                count++;
                StartCoroutine(secondsAnimation());
            }
        }
        if(text2.currentSentenceNum == 2 && !bullet)
        {
            bullet = true;
            enemy.ManualAtack();
        }
        if(text2.finished)
        {
            animator.SetBool("isTalk", false);
            StartCoroutine(StopTutorial());
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
        text.SetActive(true);
    }

    private IEnumerator secondsAnimation()
    {
        Gray.GetComponent<Animator>().SetBool("ON", false);
        text.SetActive(false);
        MessageBox.GetComponent<Animator>().SetBool("ON", false);
        yield return new WaitForSeconds(0.5f);
        cameraMove.CameraGoalFollow();
        yield return new WaitForSeconds(2f);
        Gray.GetComponent<Animator>().SetBool("ON", true);
        MessageBox.GetComponent<Animator>().SetBool("ON", true);
        yield return new WaitForSeconds(0.2f);
        text.SetActive(true);
        main2.enabled = true;
        text2.enabled = true;

    }

    private IEnumerator StopTutorial()
    {
        yield return new WaitForSeconds(1f);
        //Time.timeScale = 0f;

    }
}
