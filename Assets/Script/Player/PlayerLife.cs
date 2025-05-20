using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Image[] hearts;           // ハート3つ（UI Image）
    public string bulletTag = "bullet"; // 弾のタグ
    public string HARITag = "HARI"; // 弾のタグ
    public int life { get; private set; } = 3;
    private int maxLife = 3;



    public void TakeDamage()
    {
        if (life <= 0) return;

        life--;

        // HPオブジェクトを1つ減らす
        if (life >= 0 && life < hearts.Length)
        {
            StartCoroutine(Animate());
        }

        if (life == 0)
        {
            Debug.Log("ゲームオーバー！");
        }

    }

    public void Heal(int amount)
    {
        if (life >= maxLife) return;

        // ハートを1つ表示（最初に消えた順から戻す）
        hearts[life].enabled = true;
        life += amount;

        if (life > maxLife) life = maxLife;
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
            hearts[life].enabled = false;

            //flashInterval待ってから
            yield return new WaitForSeconds(0.15f);
            //spriteRendererをオン
            hearts[life].enabled = true;
        }

        hearts[life].enabled = false;
        yield return null;
    }
}