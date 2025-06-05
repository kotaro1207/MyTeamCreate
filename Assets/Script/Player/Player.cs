using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Grounded,     // 地面にいる
        Air,          // 空中にいる
        ShellGround,  // 甲羅状態で地面にいる
    }
    private PlayerState _state;

    [SerializeField, Header("ゲージ")] private GameObject Guage;
    [SerializeField, Header("移動速度")] private float _speed = 3;     //Unity側で設定して
    [SerializeField, Header("ジャンプ力")] private float JumpTime = 0.01f;
    [SerializeField, Header("歩き")]
    private AnimationScript walkAnimation;
    [SerializeField, Header("甲羅")]
    private AnimationScript shellAnimation;
    [SerializeField, Header("ジャンプ")]
    private JumpAnimation jumpAnimation;

    [SerializeField, Header("FadeOut Image")]
    private GameObject FadeOut;
    [SerializeField, Header("GameOver FadeOut Image")]
    private GameObject OverFadeOut;

    [SerializeField, Header("ジャンプ音")] private AudioClip jump;
    [SerializeField, Header("落下音")] private AudioClip drop;
    [SerializeField, Header("死亡")] private AudioClip dead;
    [SerializeField, Header("ダメージ音")] private AudioClip damage;
    [SerializeField, Header("不透明度")] private float transparency = 0.5f;

    private float jumpTimer = 0f;
    private bool doubleJump = false;

    public int NextjumpPower = 10;

    private Rigidbody2D _rb;
    private CameraShake cameraShake;  // CameraShakeスクリプトへの参照
    private AudioSource sound;
    private PlayerLife playerLife;
    private SpriteRenderer spriteRenderer;

    private float _move => CalculateMoveSpeed();
    private bool isOne = false;

    public int PlayerHP;
    public float JumpPower;//一時的にpublicにしてます
    public float MaxJumpPower = 15f, MinJumpPower = 5f;
    public bool isGround { get; private set; }
    public bool isWall { get; private set; }
    public bool isShell => Input.GetKey(KeyCode.Space) && isGround;
    public bool isAlive => PlayerHP >= 0;

    public bool Rock = false;
    public bool JumpRock = true;
    public bool AnimationRock = true;
    private bool first = false;

    private float CalculateMoveSpeed()
    {
        if (HPManager.Instance.Hp <= 0 || transform.position.x >= 37f)
            return 0;
        if (isShell && isGround)  //地面にいるかつ甲羅状態
            return _speed / 2;
        if (!isShell && isGround) //地面にいるとき
            return _speed;

        return _speed * 2f;     //空中
    }
    private void Awake()
    {
        sound = GetComponent<AudioSource>();
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
        // プレイヤーオブジェクトにCameraShakeスクリプトがアタッチされていることを確認
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void Update()
    {
        if (HPManager.Instance.Hp > 0)
        {
            LookHP();
            UpdateGroundStatus();
            if (!Rock)
            {
                Move();
            }
            if (!JumpRock)
            {
                Guage.SetActive(true);
                Jump();
            }
            else
            {
                Guage.SetActive(false);
            }

            if (!AnimationRock) AnimationChange();

            if (isAlive) SceneChange();
        }
        else if(HPManager.Instance.Hp == 0) 
        {
            if(!first)
            {
                first = true;
                Debug.Log("Change");
                StartCoroutine(GameOverSceneChanger());
            }
        }
    }

    private void LookHP()
    {
        if (HPManager.Instance.Hp <= 0 && !isOne)
        {
            isOne = true;
            sound.PlayOneShot(dead);
            StartCoroutine(GameOverSceneChanger());
        }
    }

    private void Move()
    {
        _rb.linearVelocity = new Vector2(_move, _rb.linearVelocity.y);

        Vector2 rayOrigin = transform.position + Vector3.right * 1.5f + Vector3.down * 1; // 足元にずらす
        float rayLength = 0.1f; // 少し長めに
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayLength, LayerMask.GetMask("Ground"));
        isWall = hit.collider != null;

        Debug.DrawRay(rayOrigin, Vector2.right * rayLength, isWall ? Color.green : Color.red);

        if (isWall)
        {
            _speed = 0f;
        }
        else
        {
            _speed = 1f;
        }
    }

    private void Jump()
    {
        float yVel = _rb.linearVelocity.y;

        if (isGround) doubleJump = false;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (jumpCount > 0 && jumpCount < MaxjumpCount)
            if (!isGround && !doubleJump)
            {
                doubleJump = true;

                _rb.linearVelocity = new(_rb.linearVelocityX, 0);
                _rb.AddForce(Vector2.up * NextjumpPower, ForceMode2D.Impulse);
            }
        }
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            jumpTimer += Time.deltaTime * 12.6f; // 5fは変動速度、調整可能
            JumpPower = Mathf.PingPong(jumpTimer, MaxJumpPower - 5f) + 5f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && isGround)
        {
            _rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            JumpPower = 0;
            jumpTimer = 0;
        }
    }


    private void AnimationChange()
    {
        if (isShell && !JumpRock)
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
            if (!JumpRock)
            {
                jumpAnimation.enabled = true;
                shellAnimation.enabled = false;
                walkAnimation.enabled = false;
            }
        }
        if (transform.position.x >= 37f && transform.position.y == -0.7737503f)
        {
            jumpAnimation.enabled = false;
            shellAnimation.enabled = false;
            walkAnimation.enabled = true;
        }
    }
    private void UpdateGroundStatus()
    {
        float rayLength = 0.1f;
        float rayOffset = 1.5f; // キャラの幅に合わせて調整（左右のRayの横位置）

        Vector2 center = (Vector2)transform.position + Vector2.down * 1.3f;
        Vector2 left = center + Vector2.left * rayOffset;
        Vector2 right = center + Vector2.right * rayOffset;

        RaycastHit2D centerHit = Physics2D.Raycast(center, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
        RaycastHit2D leftHit = Physics2D.Raycast(left, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
        RaycastHit2D rightHit = Physics2D.Raycast(right, Vector2.down, rayLength, LayerMask.GetMask("Ground"));

        isGround = centerHit.collider != null || leftHit.collider != null || rightHit.collider != null;

        Debug.DrawRay(center, Vector2.down * rayLength, centerHit.collider ? Color.green : Color.red);
        Debug.DrawRay(left, Vector2.down * rayLength, leftHit.collider ? Color.green : Color.red);
        Debug.DrawRay(right, Vector2.down * rayLength, rightHit.collider ? Color.green : Color.red);
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
                sound.PlayOneShot(damage);
                Debug.Log("hit");
            }
            else
            {
                Debug.Log("gard");
            }
        }
        else if (collision.CompareTag("HARI"))
        {
            playerLife.TakeDamage();
            cameraShake._ShakeCheck();
        }
        else if (collision.CompareTag("pithall"))
        {
            // プレイヤーのColliderを無効にすることで地面を貫通
            Collider2D playerCollider = GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                playerCollider.enabled = false;
            }

            // 下方向に力を加えて落下させる
            _rb.linearVelocity = new Vector2(0, -10f);  // 任意のスピードで下に落とす
        }
    }

    private void SceneChange()
    {
        if (transform.position.x >= 37f)
        {
            Rock = true;
            JumpRock = true;
            StartCoroutine(SceneChanger());
        }
    }

    private IEnumerator SceneChanger()
    {
        if (!isGround) yield return new WaitUntil(() => isGround == true);

        AnimationRock = true;

        yield return new WaitForSeconds(1f);

        FadeOut.SetActive(true);
    }

    private IEnumerator GameOverSceneChanger()
    {
        if (isGround) yield return new WaitUntil(() => isGround == true);

        yield return new WaitForSeconds(1f);
        OverFadeOut.SetActive(true);
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
