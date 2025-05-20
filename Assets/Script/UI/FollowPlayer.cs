using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField, Header("Player Transform")]
    private Transform PlayerTransform;

    private float Y;

    private void Awake()
    {
        Y = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector2(PlayerTransform.position.x, Y);
    }
}
