using UnityEngine;

public class StopCollision : MonoBehaviour
{
    private Bullet bullet;
    public bool isTouch = false;
    public bool ReStart = false;
    private bool one = false;
    private void Update()
    {
        if (isTouch  && ReStart && !one)
        {
            one = true;
            bullet.SpeedReturn();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            bullet = collision.gameObject.GetComponent<Bullet>();
            isTouch = true;
        }
    }
}
