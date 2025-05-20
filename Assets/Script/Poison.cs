using UnityEngine;

public class poison : MonoBehaviour
{
    public Vector3 movespeed = Vector3.zero;
    public float waittime = 10f;
    public float movetime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movetime += Time.deltaTime;
        if (waittime <= movetime)
        {
            transform.Translate(movespeed * Time.deltaTime);
        }

    }
}