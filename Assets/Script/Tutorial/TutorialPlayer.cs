using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialPlayer : MonoBehaviour
{
    public enum PlayerState
    {
        Grounded,     // 地面にいる
        Air,          // 空中にいる
        ShellGround,  // 甲羅状態で地面にいる
    }
    private PlayerState _state;

    [SerializeField] private TutorialGage guage;
    [SerializeField] private GameObject guageAll;

    [SerializeField, Header("移動速度")] private float _speed = 3;     //Unity側で設定して
    [SerializeField, Header("ジャンプ力")] private float JumpTime = 0.01f;
    private Rigidbody2D _rb;
    private CameraShake cameraShake;  // CameraShakeスクリプトへの参照

    private float _move => CalculateMoveSpeed();
    public float JumpPower = 0;//一時的にpublicにしてます
    public float MaxJumpPower = 17f;

    [SerializeField, Header("歩き")]
    private AnimationScript walkAnimation;
    [SerializeField, Header("甲羅")]
    private AnimationScript shellAnimation;
    [SerializeField, Header("ジャンプ")]
    private TutorialJumpAnimation jumpAnimation;

    [SerializeField, Header("FadeOut Image")]
    private GameObject FadeOut;

    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip difence;



    private new AudioSource audio;

    public int PlayerHP;

    private PlayerLife playerLife;
    private SpriteRenderer spriteRenderer;
    [SerializeField, Header("不透明度")] private float transparency = 0.3f;

    public bool isGround;

    public bool isShell => Input.GetKey(KeyCode.Space) && isGround;

    public bool rock = true;

    public bool moveRock = true;

    public bool ManualShell = false;

    public bool isTutorial = true;

    public bool TutorialJump = false;

    public float jumpTimer = 0;

    private float CalculateMoveSpeed()
    {
        if (HPManager.Instance.Hp <= 0 || transform.position.x >= 37f)
            return 0;
        if (isShell && isGround && !rock)  //地面にいるかつ甲羅状態
            return _speed / 2;
        if (!isShell && isGround && !rock) //地面にいるとき
            return _speed;

        return _speed * 2f;     //空中
    }
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        playerLife = GetComponent<PlayerLife>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        walkAnimation.enabled = true;
        shellAnimation.enabled = false;
        jumpAnimation.enabled = false;
    }
    void Start()
    {
        // プレイヤーオブジェクトにCameraShakeスクリプトがアタッチされていることを確認
        cameraShake = Camera.main.GetComponent<CameraShake>();

        StartCoroutine(StartStop());
    }

    private void Update()
    {
        if (ManualShell && isGround)
        {
            guage.enabled = true;
            guageAll.SetActive(true);
        }
        else
        {
            guage.enabled = false;
            guageAll.SetActive(false);
        }

        if (ManualShell)
        {
            jumpTimer += Time.deltaTime * 12.6f; // 5fは変動速度、調整可能
            JumpPower = Mathf.PingPong(jumpTimer, MaxJumpPower - 5f) + 5f;
        }
        else
        {
            jumpTimer = 0f;
            JumpPower = 0f;
        }

        UpdateGroundStatus();
        if (!moveRock) Move();
        if (!rock)
        {
            Jump();
        }
        AnimationChange();
        SceneChange();
    }


    private void Move()
    {
        _rb.linearVelocity = new Vector2(_move, _rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (ManualShell)
        {
            jumpTimer += Time.deltaTime * 12.6f; // 5fは変動速度、調整可能
            JumpPower = Mathf.PingPong(jumpTimer, MaxJumpPower - 5f) + 5f;
        }
    }

    public void ManualJump()
    {
        audio.PlayOneShot(jump);
        Debug.Log("ジャンプ");
        _rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
    }
    public void DoubleJump()
    {
        audio.PlayOneShot(jump);
        _rb.linearVelocity = new(_rb.linearVelocityX, 0);
        _rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    private void AnimationChange()
    {
        if (!isGround)
        {
            jumpAnimation.enabled = true;
            shellAnimation.enabled = false;
            walkAnimation.enabled = false;
        }
        else if (ManualShell)
        {
            //Debug.Log("どｔｔｔｔ");
            shellAnimation.enabled = true;
            walkAnimation.enabled = false;
            jumpAnimation.enabled = false;
        }
        else if (!ManualShell)
        {
            walkAnimation.enabled = true;
            shellAnimation.enabled = false;
            jumpAnimation.enabled = false;
        }
        else if (isShell && !rock)
        {
            shellAnimation.enabled = true;
            walkAnimation.enabled = false;
            jumpAnimation.enabled = false;
        }
        else if (isGround && !rock)
        {
            walkAnimation.enabled = true;
            shellAnimation.enabled = false;
            jumpAnimation.enabled = false;
        }

        if (transform.position.x >= 37f && transform.position.y == -0.7737503f)
        {
            jumpAnimation.enabled = false;
            shellAnimation.enabled = false;
            walkAnimation.enabled = false;
        }
    }
    private void UpdateGroundStatus()//接地しているかどうか
    {
        Vector2 rayOrigin = transform.position + Vector3.down * 1.3f; // 足元にずらす
        float rayLength = 0.1f; // 少し長めに
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
        isGround = hit.collider != null;

        Debug.DrawRay(rayOrigin, Vector2.down * rayLength, isGround ? Color.green : Color.red);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            if (ManualShell)
            {
                Debug.Log("ガードできた");
                audio.PlayOneShot(difence);
            }
            else if (!isShell)
            {
                cameraShake._ShakeCheck();
                if (!isTutorial)
                {
                    playerLife.TakeDamage();
                    StartCoroutine(InvisibleAnimation());
                }
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

        Debug.Log("よんだ");
        FadeOut.SetActive(true);
    }

    private IEnumerator StartStop()
    {
        yield return new WaitForSeconds(1f);
        walkAnimation.enabled = false;
    }

    private IEnumerator InvisibleAnimation()//チカチカアニメーション&無敵処理
    {
        float elapsed = 0f;
        float duration = 1f;

        Physics2D.IgnoreLayerCollision(7, 8, true);//PlayerとDamageを与えるオブジェクトの当たり判定を一時的にFalseにする

        while (elapsed < duration)
        {
            elapsed += 0.2f;

            //flashInterval待ってから
            yield return new WaitForSeconds(0.15f);

            //spriteRendererをオフ
            //spriteRenderer.enabled = false;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, transparency);
            //flashInterval待ってから
            yield return new WaitForSeconds(0.15f);
            //spriteRendererをオン
            //spriteRenderer.enabled = true;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }

        Physics2D.IgnoreLayerCollision(7, 8, false);//PlayerとDamageを与えるオブジェクトの当たり判定を戻す。

    }
}
