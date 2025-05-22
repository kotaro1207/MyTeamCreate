using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// 攻撃を実行する間隔の範囲
    [SerializeField, Header("最小間隔")] float minAttackInterval = 1f;
    [SerializeField, Header("最大間隔")] float maxAttackInterval = 3f;

    // 発射するオブジェクト（bullet）
    [SerializeField, Header("発射するオブジェクトのプレハブ")] GameObject bulletPrefab;  // 発射するオブジェクトのプレハブ

    // 発射位置
    [SerializeField, Header("発射する位置")] Transform firePoint;

    [SerializeField] GunMove gun;

    [SerializeField] AnimationScript _animation;

    private GameObject player;

    private float attackTimer;  // タイマー

    public bool isTutorial { get; private set; } = false;

    void Start()
    {
        // 最初の攻撃間隔をランダムに決定
        SetRandomAttackInterval();

        player = GameObject.FindWithTag("Player");

    }

    void Update()
    {
        // タイマーを進める
        attackTimer -= Time.deltaTime;

        if (!isTutorial)
        {
            // 攻撃タイミング
            if (attackTimer <= 0f && player.transform.position.x <= 37f)
            {
                Attack();

                gun.Recoil();               //銃の反動アニメーション

                SetRandomAttackInterval();  // 新しいランダムな攻撃間隔を設定
            }
            else if (player.transform.position.x >= 34.45f)
            {
                _animation.enabled = false;
            }
        }
    }

    // 攻撃処理
    void Attack()
    {
        // 発射するオブジェクト（bullet）を生成
        if (bulletPrefab != null && firePoint != null)
        {
            // 弾を生成
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Rigidbody2D を取得して発射力を加える
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        }

    }

    // 攻撃間隔をランダムに設定
    void SetRandomAttackInterval()
    {
        attackTimer = Random.Range(minAttackInterval, maxAttackInterval);
    }

    public void ManualAtack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Rigidbody2D を取得して発射力を加える
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    }

    // プレイヤーと衝突した場合
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Hit: " + collision.name); // 何に当たったかログ

        if (collision.CompareTag("Player"))
        {
            // ここで敵が消える処理を追加
            Destroy(gameObject);
        }
    }
}