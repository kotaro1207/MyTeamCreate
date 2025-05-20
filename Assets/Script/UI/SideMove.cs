using UnityEngine;

public class SideMove : MonoBehaviour
{
    [SerializeField, Header("画像")]
    private GameObject obj;

    [SerializeField, Header("動くスピード")]
    private float speed = 0.1f;
    private float moveSpeed => isLeft ? -speed : speed;

    public bool isLeft = false;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        this.transform.Translate(moveSpeed, 0, 0);

        if (obj.transform.position.x <= 0f && isLeft)
        {
            this.transform.position = new Vector3(obj.transform.position.x + 19.5f, transform.position.y, 0);
        }
        else if(obj.transform.position.x >= -1f && !isLeft)
        {
            this.transform.position = new Vector3(obj.transform.position.x - 19.5f, transform.position.y, 0);
        }
    }
}
