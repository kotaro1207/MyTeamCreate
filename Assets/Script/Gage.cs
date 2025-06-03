using UnityEngine;

public class Gage : MonoBehaviour
{
    public float moveSpeed = 8f;            // ゲージの移動速度
    public float targetHeight = 3f;         // 上下運動の高さ

    private float initialLocalY;            // ゲージのローカルY位置
    private bool movingUp = true;           // 上昇中かどうか

    [SerializeField] private Transform mask;    // Mask の Transform
    void Start()
    {
        initialLocalY = transform.localPosition.y; // ローカル座標で記録
    }

    void Update()
    {
        Vector3 localPos = transform.localPosition;

        // スペースキーで上下に動く
        if (Input.GetKey(KeyCode.Space))
        {
            if (movingUp)
            {
                localPos.y += moveSpeed * Time.deltaTime;
                if (localPos.y >= initialLocalY + targetHeight)
                {
                    movingUp = false;
                }
            }
            else
            {
                localPos.y -= moveSpeed * Time.deltaTime;
                if (localPos.y <= initialLocalY)
                {
                    movingUp = true;
                }
            }
            transform.localPosition = localPos;
        }
        else
        {
            // スペースを離したら初期位置に戻す
            if (localPos.y > initialLocalY)
            {
                localPos.y -= moveSpeed * Time.deltaTime;
                if (localPos.y < initialLocalY) localPos.y = initialLocalY;
            }
            else if (localPos.y < initialLocalY)
            {
                localPos.y += moveSpeed * Time.deltaTime;
                if (localPos.y > initialLocalY) localPos.y = initialLocalY;
            }
            transform.localPosition = localPos;
        }
        // Mask を Gage のローカル位置に同期（ジャンプでズレないようにする）
        if (mask != null)
        {
            Vector3 maskLocalPos = mask.localPosition;
            maskLocalPos.y = transform.localPosition.y;
            mask.localPosition = maskLocalPos;
        }
    }
    private void OnDisable()
    {
        Vector3 pos = transform.localPosition;
        pos.y = initialLocalY;
        transform.localPosition = pos;
    }

}