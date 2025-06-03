using UnityEngine;

public class Gage : MonoBehaviour
{
    public float moveSpeed = 8f;            // �Q�[�W�̈ړ����x
    public float targetHeight = 3f;         // �㉺�^���̍���

    private float initialLocalY;            // �Q�[�W�̃��[�J��Y�ʒu
    private bool movingUp = true;           // �㏸�����ǂ���

    [SerializeField] private Transform mask;    // Mask �� Transform
    void Start()
    {
        initialLocalY = transform.localPosition.y; // ���[�J�����W�ŋL�^
    }

    void Update()
    {
        Vector3 localPos = transform.localPosition;

        // �X�y�[�X�L�[�ŏ㉺�ɓ���
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
            // �X�y�[�X�𗣂����珉���ʒu�ɖ߂�
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
        // Mask �� Gage �̃��[�J���ʒu�ɓ����i�W�����v�ŃY���Ȃ��悤�ɂ���j
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