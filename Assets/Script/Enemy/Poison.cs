using UnityEngine;
public class poison : MonoBehaviour
{
    public Vector3 movespeed = Vector3.zero;
    public Player player;
    float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && player.isGround && !player.Rock && time < 3)
        {
            transform.Translate(movespeed * Time.deltaTime);
        }
    }
}