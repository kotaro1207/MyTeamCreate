using UnityEngine;

public class Wurning : MonoBehaviour
{
    private Animator anim;
    private bool first = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    private void Update()
    {
        if (HPManager.Instance.Hp == 1 && !first)
        {
            first = true;
            anim.enabled = true;
        }
    }
}
