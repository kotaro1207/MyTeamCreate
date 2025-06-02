using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Yadotyumuri : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Triangle;
    [SerializeField] private GameObject MessageBox;
    [SerializeField] private GameObject textMesh;
    [SerializeField] private OneTextController text1;
    [SerializeField] private TwoTextController text2;
    [SerializeField] private ThirdTextController text3;
    [SerializeField] private FourTextController text4;
    [SerializeField] private OneTutorialMain main1;
    [SerializeField] private TwoTutorialMain main2;
    [SerializeField] private ThirdTutorialMain main3;
    [SerializeField] private FourTutorialMain main4;
    [SerializeField] private TutorialEnemy enemy;
    [SerializeField] private TutorialCameraMove cameraMove;
    [SerializeField] private StopCollision stopCollision;
    [SerializeField] private Image Gray;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject Musk;
    [SerializeField] private GameObject ButtonUI;
    [SerializeField] private GameObject HandUI;
    [SerializeField] private GameObject FadeOut;
    private int count = 0;

    bool bullet = false;
    public bool isBullet = false;
    bool thierd = false;
    bool once = false;
    //bool guageMusk = false;
    bool touch = false;
    bool finished = false;
    bool Jump = false;
    bool isMusk = false;
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
        if (text1.finished && !once)
        {
            StartCoroutine(LateSpeakStop());
            text1.enabled = false;
            main1.enabled = false;

            if (Input.GetKeyDown(KeyCode.Space) && count == 0)
            {
                Triangle.GetComponent<Image>().enabled = false;
                Triangle.GetComponent<Animator>().enabled = false;
                count++;
                StartCoroutine(secondsAnimation());
                once = true;
            }
        }
        if (text2.currentSentenceNum == 2 && !bullet)
        {
            StartCoroutine(BulletFire());
        }
        if(stopCollision.isTouch && !touch)
        {
            touch = true;

            StartCoroutine(OnTriangle());
        }
        if (text2.finished && !thierd)
        {
            thierd = true;
            StartCoroutine(LateSpeakStop());
            StartCoroutine(ThierdAnimation());
        }
        if(text3.currentSentenceNum == 4 && Input.GetKeyDown(KeyCode.Space) && !Jump)
        {
            Jump = true;
            StartCoroutine(JumpStart());
            StartCoroutine(LateSpeakStop());
        }
        if(text3.currentSentenceNum == 2 && Input.GetKeyDown(KeyCode.Space) && !isMusk)
        {
            isMusk = true;
            StartCoroutine(GuageMusk());
        }
        if(text3.finished && Input.GetKeyDown(KeyCode.Space) && !finished)
        {
            StartCoroutine(LateSpeakStop());
            MessageBox.GetComponent<Animator>().SetBool("ON", false);
            Triangle.GetComponent<Image>().enabled = false;
            Triangle.GetComponent<Animator>().enabled = false;
            textMesh.SetActive(false);
            main2.enabled = false;
            text2.enabled = false;

            finished = true;
            FadeOut.GetComponent<Animator>().enabled = true;
        }
    }

    private IEnumerator LateSpeakStop()
    {
        yield return new WaitForSeconds(0.5f);

        animator.SetBool("isTalk", false);
    }

    private IEnumerator OnTriangle()
    {
        MessageBox.GetComponent<Animator>().SetBool("ON", true);
        animator.SetBool("isTalk", true);
        yield return new WaitForSeconds(0.2f);
        main2.enabled = true;
        text2.enabled = true;
        text2.ManualNext();
        yield return new WaitForSeconds(0.1f);
        textMesh.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        Triangle.GetComponent<Image>().enabled = true;
        Triangle.GetComponent<Animator>().enabled = true;
    }

    private IEnumerator CameraInMove()
    {
        yield return new WaitForSeconds(1f);
        Gray.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        MessageBox.GetComponent<Animator>().enabled = true;
        MessageBox.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        text1.enabled = true;
        main1.enabled = true;
        textMesh.SetActive(true);
        yield return new WaitForSeconds(1f);
        animator.enabled = true;
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
        panel.GetComponent<Animator>().SetBool("ON", false);
        bullet = true;
        yield return new WaitForSeconds(0.75f);
        enemy.ManualAtack();
        yield return new WaitForSeconds(1f);
        StartCoroutine(LateSpeakStop());
        MessageBox.GetComponent<Animator>().SetBool("ON", false);
        Triangle.GetComponent<Image>().enabled = false;
        Triangle.GetComponent<Animator>().enabled = false;
        textMesh.SetActive(false);
        main2.enabled = false;
        text2.enabled = false;
    }

    private IEnumerator ThierdAnimation()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        MessageBox.GetComponent<Animator>().SetBool("ON", false);
        textMesh.SetActive(false);
        main2.enabled = false;
        text2.enabled = false;
        Triangle.GetComponent<Image>().enabled = false;
        Triangle.GetComponent<Animator>().enabled = false;

        yield return new WaitForSeconds(0.25f);
        ButtonUI.GetComponent<Image>().enabled = true;
        ButtonUI.GetComponent<Animator>().enabled = true;
        HandUI.GetComponent<Image>().enabled = true;
        HandUI.GetComponent<Animator>().enabled = true;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        ButtonUI.GetComponent<Image>().enabled = false;
        ButtonUI.GetComponent<Animator>().enabled = false;
        HandUI.GetComponent<Image>().enabled = false;
        HandUI.GetComponent<Animator>().enabled = false;


        player.GetComponent<TutorialPlayer>().ManualShell = true;
        yield return new WaitForSeconds(0.1f);
        stopCollision.ReStart = true;
        yield return new WaitForSeconds(1.5f);

        animator.SetBool("isTalk", true);
        MessageBox.GetComponent<Animator>().SetBool("ON", true);
        yield return new WaitForSeconds(0.2f);
        textMesh.SetActive(true);
        textMesh.GetComponent<TextMeshProUGUI>().text = " ";
        text3.enabled = true;
        main3.enabled = true;
        yield return new WaitForSeconds(0.25f);
        Triangle.GetComponent<Image>().enabled = true;
        Triangle.GetComponent<Animator>().enabled = true;

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator GuageMusk()
    {
        yield return new WaitForSeconds(0.5f);

        Musk.GetComponent<RectTransform>().position = new Vector3(220f, 430f, 0);

        panel.GetComponent<Animator>().SetBool("ON", true);
    }

    private IEnumerator JumpStart()
    {
        panel.GetComponent<Animator>().SetBool("ON", false);

        yield return new WaitForSeconds(1f);

        player.GetComponent<TutorialPlayer>().ManualShell = false;
        player.GetComponent<TutorialPlayer>().TutorialJump = true;
        player.GetComponent<TutorialPlayer>().ManualJump();

        yield return new WaitUntil(() => player.GetComponent<TutorialPlayer>().isGround = true);
        player.GetComponent<TutorialPlayer>().TutorialJump = false;
    }

}
