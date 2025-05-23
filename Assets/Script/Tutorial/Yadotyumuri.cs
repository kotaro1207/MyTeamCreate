using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Yadotyumuri : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject MessageBox;
    [SerializeField]private Image Gray;

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
        
    }
    private IEnumerator CameraInMove()
    {
        yield return new WaitForSeconds(1f);
        Gray.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        animator.enabled = true;
        MessageBox.SetActive(true);
        new WaitForSeconds(0.15f);
        animator.SetBool("isTalk",true);
    }
}
