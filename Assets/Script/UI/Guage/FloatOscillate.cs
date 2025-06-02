using UnityEngine;

public class FloatOscillate : MonoBehaviour
{
    public float moveSpeed = 2f;           // 移動速度
    public float targetHeight = 3f;        // 上下に動く高さ
    private float initialY;                // 初期Y座標
    private bool movingUp = true;          // 上昇中かどうか

    void Start()
    {
        initialY = transform.position.y;   // 初期位置を記録
    }

    void Update()
    {
        float currentY = transform.position.y;

        if (Input.GetKey(KeyCode.Space))
        {
            // 上下運動を繰り返す
            if (movingUp)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
                if (currentY >= initialY + targetHeight)
                {
                    movingUp = false;
                }
            }
            else
            {
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
                if (currentY <= initialY)
                {
                    movingUp = true;
                }
            }
        }
        else
        {
            // スペースキーを離したら初期位置へ戻る
            if (currentY > initialY)
            {
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            }
            else if (currentY < initialY)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
        }
    }
}