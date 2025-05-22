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


    public float recoilAngle = -10f;
    public float recoilSpeed = 20f;

    void Update()
    {
        //LookAtTarget();
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
        Vector3 heading = player.transform.position - transform.position;

        Quaternion targetRotation = Quaternion.FromToRotation(fromDirection, heading.normalized);

        Vector3 targetEulerAngles = targetRotation.eulerAngles;

        // Z軸の角度は0〜360度なので、-180〜180度に直す
        float z = targetEulerAngles.z;
        if (z > 180f) z -= 360f;

        // -30〜30度に制限
        z = Mathf.Clamp(z, -30f, 30f);

        targetEulerAngles.z = z;
        targetRotation = Quaternion.Euler(targetEulerAngles);

        //targetRotation = Mathf.Clamp(targetRotation, -30,30);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Playerに命中！");
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "ground")
        {
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
