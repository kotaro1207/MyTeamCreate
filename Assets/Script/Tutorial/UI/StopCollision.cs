using UnityEngine;

public class StopCollision : MonoBehaviour
{
    public bool isTouch = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("bullet"))
        {
            isTouch = true;
        }
    }
}
