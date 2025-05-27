using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialPlayer : MonoBehaviour
{
    public enum PlayerState
    {
        Grounded,     // �n�ʂɂ���
        Air,          // �󒆂ɂ���
        ShellGround,  // �b����ԂŒn�ʂɂ���
    }
    private PlayerState _state;

    [SerializeField, Header("�ړ����x")] private float _speed = 3;     //Unity���Őݒ肵��
    [SerializeField, Header("�W�����v��")] private float JumpTime = 0.01f;
    private Rigidbody2D _rb;
    private CameraShake cameraShake;  // CameraShake�X�N���v�g�ւ̎Q��

    private float _move => CalculateMoveSpeed();
    public float JumpPower;//�ꎞ�I��public�ɂ��Ă܂�
    public float MaxJumpPower = 15f;

    [SerializeField, Header("����")]
    private AnimationScript walkAnimation;
    [SerializeField, Header("�b��")]
    private AnimationScript shellAnimation;
    [SerializeField, Header("�W�����v")]
    private TutorialJumpAnimation jumpAnimation;

    [SerializeField, Header("FadeOut Image")]
    private GameObject FadeOut;

    public int PlayerHP;

    private PlayerLife playerLife;
    private SpriteRenderer spriteRenderer;
    [SerializeField, Header("�s�����x")] private float transparency = 0.3f;

    public bool isGround { get; private set; }

    public bool isShell => Input.GetKey(KeyCode.Space) && isGround;

    public bool rock = true;

    public bool moveRock = true;

    private float CalculateMoveSpeed()
    {
        if (playerLife.life <= 0 || transform.position.x >= 37f)
            return 0;
        if (isShell && isGround && !rock)  //�n�ʂɂ��邩�b�����
            return _speed / 2;
        if (!isShell && isGround && !rock) //�n�ʂɂ���Ƃ�
            return _speed;

        return _speed * 2f;     //��
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerLife = GetComponent<PlayerLife>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        JumpPower = 0;

        walkAnimation.enabled = true;
        shellAnimation.enabled = false;
        jumpAnimation.enabled = false;
    }
    void Start()
    {
        // �v���C���[�I�u�W�F�N�g��CameraShake�X�N���v�g���A�^�b�`����Ă��邱�Ƃ��m�F
        cameraShake = Camera.main.GetComponent<CameraShake>();

        StartCoroutine(StartStop());
    }

    private void Update()
    {
        LookHP();
        UpdateGroundStatus();
        if(!moveRock)Move();
        if (!rock)
        {
            Jump();
            AnimationChange();
        }
        SceneChange();
    }

    private void LookHP()
    {
        PlayerHP = playerLife.life;
    }

    private void Move()
    {
        _rb.linearVelocity = new Vector2(_move, _rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (transform.position.x <= 37f)
        {
            if (Input.GetKey(KeyCode.Space) && isGround)
            {
                JumpPower += JumpTime;
                if (JumpPower > MaxJumpPower)
                {
                    JumpPower = MaxJumpPower;
                }
                else if (JumpPower < 5)
                {
                    JumpPower = 5f;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                JumpPower = 0;
            }
        }
    }

    private void AnimationChange()
    {
        if (isShell)
        {
            shellAnimation.enabled = true;
            walkAnimation.enabled = false;
            jumpAnimation.enabled = false;
        }
        else if (isGround)
        {
            walkAnimation.enabled = true;
            shellAnimation.enabled = false;
            jumpAnimation.enabled = false;
        }
        else
        {
            jumpAnimation.enabled = true;
            shellAnimation.enabled = false;
            walkAnimation.enabled = false;
        }

        if (transform.position.x >= 37f && transform.position.y == -0.7737503f)
        {
            jumpAnimation.enabled = false;
            shellAnimation.enabled = false;
            walkAnimation.enabled = false;
        }
    }
    private void UpdateGroundStatus()//�ڒn���Ă��邩�ǂ���
    {
        Vector2 rayOrigin = transform.position + Vector3.down * 1.3f; // �����ɂ��炷
        float rayLength = 0.1f; // �������߂�
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
        isGround = hit.collider != null;

        Debug.DrawRay(rayOrigin, Vector2.down * rayLength, isGround ? Color.green : Color.red);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            if (!isShell)
            {
                cameraShake._ShakeCheck();
                playerLife.TakeDamage();
                StartCoroutine(InvisibleAnimation());
                Debug.Log("hit");
            }
            else
            {
                Debug.Log("gard");
            }
        }
        else if (collision.CompareTag("HARITag"))
        {
            playerLife.TakeDamage();
            cameraShake._ShakeCheck();
        }
    }

    private void SceneChange()
    {
        if (transform.position.x >= 37f)
        {
            StartCoroutine(SceneChanger());
        }
    }

    private IEnumerator SceneChanger()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("���");
        FadeOut.SetActive(true);
    }

    private IEnumerator StartStop()
    {
        yield return new WaitForSeconds(1f);
        walkAnimation.enabled = false;
    }

    private IEnumerator InvisibleAnimation()//�`�J�`�J�A�j���[�V����&���G����
    {
        float elapsed = 0f;
        float duration = 1f;

        Physics2D.IgnoreLayerCollision(7, 8, true);//Player��Damage��^����I�u�W�F�N�g�̓����蔻����ꎞ�I��False�ɂ���

        while (elapsed < duration)
        {
            elapsed += 0.2f;

            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(0.15f);

            //spriteRenderer���I�t
            //spriteRenderer.enabled = false;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, transparency);
            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(0.15f);
            //spriteRenderer���I��
            //spriteRenderer.enabled = true;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }

        Physics2D.IgnoreLayerCollision(7, 8, false);//Player��Damage��^����I�u�W�F�N�g�̓����蔻���߂��B

    }
}
