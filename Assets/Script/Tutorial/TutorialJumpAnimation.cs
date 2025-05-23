using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TutorialJumpAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameObject player;
    private TutorialPlayer playerScript;
    private Rigidbody2D rb;

    [Header("ジャンプ状態ごとのスプライト")]
    public Sprite spriteJumpStart;  // 画像1
    public Sprite spriteJumpUp;     // 画像2
    public Sprite spriteJumpDown;   // 画像3

    private float jumpElapsedTime = 0f;
    private bool wasGroundedLastFrame = true;

    private void Awake()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerScript = GetComponent<TutorialPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        jumpElapsedTime = 0f;
    }
    private void Update()
    {

        bool isGrounded = playerScript.isShell;
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