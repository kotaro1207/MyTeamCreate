using UnityEngine;
using System.Collections;
public class DeadAnimationScript : MonoBehaviour
{
    public Sprite[] sprites;
    public float framerate = 1f / 6f;
    [SerializeField] private GameObject OverFadeOut;
    [SerializeField] private Player player;

    private SpriteRenderer spriteRenderer;
    private int frame;
    private int HP;
    private bool isDead = false;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("bullet") || collision.CompareTag("HARI"))
        {
            if (HPManager.Instance.Hp <= 0)
            {
                Die();
            }
        }
        else if (collision.CompareTag("poison"))
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        frame = 0;

        // AnimationScript を削除
        AnimationScript[] animScripts = GetComponents<AnimationScript>();
        JumpAnimation[] JuanimScripts = GetComponents<JumpAnimation>();
        Player[] PlayerScripts = GetComponents<Player>();

        foreach (var animScript in animScripts)
        {
            Destroy(animScript);
        }

        foreach (var JuanimScript in JuanimScripts)
        {
            Destroy(JuanimScript);
        }

        foreach (var PlayerScript in PlayerScripts)
        {
            Destroy(PlayerScript);
        }

        Debug.Log("死亡アニメーションに移行します");
        InvokeRepeating(nameof(PlayDeathAnimation), 0f, framerate);
    }

    private void PlayDeathAnimation()
    {
        if (frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
            Debug.Log($"Showing frame {frame}");
            frame++;
        }
        else
        {
            Debug.Log("Animation complete. Destroying object.");
            CancelInvoke(nameof(PlayDeathAnimation));
            StartCoroutine(GameOverSceneChanger());
        }
    }
    private IEnumerator GameOverSceneChanger()
    {
        if (player.isGround) yield return new WaitUntil(() => player.isGround == true);

        OverFadeOut.SetActive(true);
    }

}
