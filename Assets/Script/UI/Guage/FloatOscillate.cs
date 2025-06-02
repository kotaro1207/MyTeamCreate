using UnityEngine;

public class FloatOscillate : MonoBehaviour
{
    public float moveSpeed = 2f;           // �ړ����x
    public float targetHeight = 3f;        // �㉺�ɓ�������
    private float initialY;                // ����Y���W
    private bool movingUp = true;          // �㏸�����ǂ���

    void Start()
    {
        initialY = transform.position.y;   // �����ʒu���L�^
    }

    void Update()
    {
        float currentY = transform.position.y;

        if (Input.GetKey(KeyCode.Space))
        {
            // �㉺�^�����J��Ԃ�
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
            // �X�y�[�X�L�[�𗣂����珉���ʒu�֖߂�
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