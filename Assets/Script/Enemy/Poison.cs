using UnityEngine;
public class poison : MonoBehaviour
{
    public Vector3 movespeed = Vector3.zero;
    public Player player;

    void Update()
    {
        if(Input.GetKey(KeyCode.Space)&&player.isGround)
        {
            transform.Translate(movespeed * Time.deltaTime);
        }
    }
}