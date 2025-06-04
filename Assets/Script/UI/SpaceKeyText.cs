using UnityEngine;

public class SpaceKeyText : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        this.gameObject.SetActive(true);
        anim.enabled = false;
    }

    public void WakeAnimator()
    {
        this.gameObject.SetActive(true);
        anim.enabled = true;
    }

    public void SleepAnimator()
    {
        this.gameObject.SetActive(false);
        anim.enabled = false;
    }
}
