using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Image[] hearts;           // ハート3つ（UI Image）
    public string bulletTag = "bullet"; // 弾のタグ
    public string HARITag = "HARI"; // 弾のタグ
    //public int life { get; private set; }

    private int maxLife = 3;

    [SerializeField, Header("不透明度")] private float transparency = 0.5f;

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

        // HPオブジェクトを1つ減らす
        if (HPManager.Instance.Hp >= 0 && HPManager.Instance.Hp < hearts.Length)
        {
            StartCoroutine(Animate());
        }

        if (HPManager.Instance.Hp == 0)
        {
            Debug.Log("ゲームオーバー！");
        }

    }

    public void Heal(int amount = 1)
    {
        // ハートを1つ表示（最初に消えた順から戻す）
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

            //flashInterval待ってから
            yield return new WaitForSeconds(0.15f);

            //spriteRendererをオフ
            hearts[HPManager.Instance.Hp].color = new Color(hearts[HPManager.Instance.Hp].color.r, hearts[HPManager.Instance.Hp].color.g, hearts[HPManager.Instance.Hp].color.b, transparency);


            //flashInterval待ってから
            yield return new WaitForSeconds(0.15f);
            //spriteRendererをオン
            hearts[HPManager.Instance.Hp].color = new Color(hearts[HPManager.Instance.Hp].color.r, hearts[HPManager.Instance.Hp].color.g, hearts[HPManager.Instance.Hp].color.b, 1f);
        }

        hearts[HPManager.Instance.Hp].enabled = false;
        yield return null;
    }
}