using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TutorialJumpAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameObject player;
    private TutorialPlayer playerScript;
    private Rigidbody2D rb;

    [Header("�W�����v��Ԃ��Ƃ̃X�v���C�g")]
    public Sprite spriteJumpStart;  // �摜1
    public Sprite spriteJumpUp;     // �摜2
    public Sprite spriteJumpDown;   // �摜3

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

        // --- �󒆂ł� Animator �𖳌������ĉ摜�ɐ؂�ւ� ---
        if (!isGrounded)
        {
            jumpElapsedTime += Time.deltaTime;

            if (jumpElapsedTime <= 0.15f)
            {
                spriteRenderer.sprite = spriteJumpStart;  // �W�����v����
            }
            else if (yVel > 0.1f)
            {
                spriteRenderer.sprite = spriteJumpUp;     // �㏸��
            }
            else
            {
                spriteRenderer.sprite = spriteJumpDown;   // ���~��
            }

        }

        wasGroundedLastFrame = isGrounded;
    }
}