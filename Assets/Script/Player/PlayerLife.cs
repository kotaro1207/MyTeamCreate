using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Image[] hearts;           // �n�[�g3�iUI Image�j
    public string bulletTag = "bullet"; // �e�̃^�O
    public string HARITag = "HARI"; // �e�̃^�O
    //public int life { get; private set; }

    private int maxLife = 3;

    [SerializeField, Header("�s�����x")] private float transparency = 0.5f;

    private void Update()
    {
        if (HPManager.Instance != null)
        {
            //life = HPManager.Instance.Hp;
        }
    }

    public void TakeDamage()
    {
        if (HPManager.Instance.Hp <= 0) return;

        HPManager.Instance.Hp--;

        // HP�I�u�W�F�N�g��1���炷
        if (HPManager.Instance.Hp >= 0 && HPManager.Instance.Hp < hearts.Length)
        {
            StartCoroutine(Animate());
        }

        if (HPManager.Instance.Hp == 0)
        {
            Debug.Log("�Q�[���I�[�o�[�I");
        }

    }

    public void Heal(int amount = 1)
    {
        // �n�[�g��1�\���i�ŏ��ɏ�����������߂��j
        hearts[HPManager.Instance.Hp].enabled = true;
        HPManager.Instance.Hp += amount;

        if (HPManager.Instance.Hp > maxLife) HPManager.Instance.Hp = maxLife;
    }

    private IEnumerator Animate()
    {

        float elapsed = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            elapsed += 0.2f;

            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(0.15f);

            //spriteRenderer���I�t
            hearts[HPManager.Instance.Hp].color = new Color(hearts[HPManager.Instance.Hp].color.r, hearts[HPManager.Instance.Hp].color.g, hearts[HPManager.Instance.Hp].color.b, transparency);


            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(0.15f);
            //spriteRenderer���I��
            hearts[HPManager.Instance.Hp].color = new Color(hearts[HPManager.Instance.Hp].color.r, hearts[HPManager.Instance.Hp].color.g, hearts[HPManager.Instance.Hp].color.b, 1f);
        }

        hearts[HPManager.Instance.Hp].enabled = false;
        yield return null;
    }
}