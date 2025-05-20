using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class JumpAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Player player;
    private Rigidbody2D rb;

    [Header("ジャンプ状態ごとのスプライト")]
    public Sprite spriteJumpStart;  // 画像1
    public Sprite spriteJumpUp;     // 画像2
    public Sprite spriteJumpDown;   // 画像3

    private float jumpElapsedTime = 0f;
    private bool wasGroundedLastFrame = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        jumpElapsedTime = 0f;
    }
    private void Update()
    {

        bool isGrounded = player.isShell;
        float yVel = rb.linearVelocity.y;

        // --- 空中では Animator を無効化して画像に切り替え ---
        if (!isGrounded)
        {
            jumpElapsedTime += Time.deltaTime;

            if (jumpElapsedTime <= 0.15f)
            {
                spriteRenderer.sprite = spriteJumpStart;  // ジャンプ直後
            }
            else if (yVel > 0.1f)
            {
                spriteRenderer.sprite = spriteJumpUp;     // 上昇中
            }
            else
            {
                spriteRenderer.sprite = spriteJumpDown;   // 下降中
            }

        }

        wasGroundedLastFrame = isGrounded;
    }
}