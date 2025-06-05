using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;                  // 弾のスピード
    public float lockYThreshold = -3.0f;      // Y軸を固定する高さ

    private Vector2 moveDirection;            // 一度だけ計算する移動方向
    private Rigidbody2D rb;
    private bool yLocked = false;
    private float lockedY;
    private float previousY;
    private GameObject player;                // プレイヤーオブジェクトの参照
    private bool isPlayerAlive = true;        // プレイヤーが生きているかどうかをチェック

    [SerializeField] private float rotationSpeed;

    [SerializeField] private Vector3 fromDirection;

    [SerializeField] private GameObject HitUI;

    private new AudioSource audio;

    public float recoilAngle = -10f;
    public float recoilSpeed = 20f;

    public bool DontDestroy = false;

    public bool isTouch = false;

    void Update()
    {
        LookAtTarget();
    }
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        // プレイヤーの現在位置を取得して向きを決定
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 targetPos = player.transform.position;
            moveDirection = (targetPos - (Vector2)transform.position).normalized;
        }
        else
        {
            // プレイヤーが見つからなければ直進
            moveDirection = Vector2.left;
            isPlayerAlive = false;  // プレイヤーがいない場合は、弾を動かさない
        }

        rb = GetComponent<Rigidbody2D>();
        previousY = transform.position.y;
    }

    private void LookAtTarget()
    {
        if (player == null) return;

        //プレイヤー方向を使って角度を算出
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        //上向きのスプライト → -90°の補正が必要
        angle -= 180f;

        //回転適用
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    void FixedUpdate()
    {
        // プレイヤーが消えたかどうかを毎フレームチェック
        if (player == null)
        {
            isPlayerAlive = false;
        }

        if (isPlayerAlive)
        {
            // Y軸ロック処理（上から落ちる設定に基づき）
            if (!yLocked && previousY > lockYThreshold && transform.position.y <= lockYThreshold)
            {
                yLocked = true;
                lockedY = transform.position.y;

                rb.gravityScale = 0f;
                rb.linearVelocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (yLocked)
            {
                // Y座標を固定しながら進行方向に進む（Yは変わらない）
                Vector3 pos = transform.position;
                pos.y = lockedY;
                transform.position = pos;

                rb.linearVelocity = new Vector2(moveDirection.x * speed, 0f); // X方向のみ
            }
            else
            {
                // 通常の移動（追尾しない）
                rb.linearVelocity = moveDirection * speed;
            }
        }
        else
        {
            // プレイヤーがいない場合は弾を停止
            rb.linearVelocity = Vector2.zero;
        }
        previousY = transform.position.y;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 hitPosition = transform.position; // 衝突した位置

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Playerに命中！");
            if (HitUI != null && player != null)
            {
                GameObject ui = Instantiate(HitUI, transform.position, Quaternion.identity);
                ui.transform.SetParent(player.transform); // Playerの子にする
                ui.transform.localPosition = Vector3.zero; // 相対位置を調整（必要なら）
                Destroy(ui, 0.1f); // 1秒後に消す
            }
            Destroy(gameObject);
        }
        if (collision.CompareTag("ground"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("StopCollision"))
        {
            isTouch = true;
            speed = 0f;
        }



    }
    private void OnBecameInvisible()
    {
        if (!DontDestroy) Destroy(gameObject);
    }

    public void SpeedReturn()
    {
        speed = 17f;
    }
}
