using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Header("PlayerÇÃTransform")]
    private Transform target;
    [SerializeField, Header("Ç∏ÇÁÇµ")]
    private Vector3 offsetX = new Vector3(7, 0, -10);

    private CameraShake cameraShake;
    private float fixedY = 0f;
    private float offsetZ = -10f;

    [SerializeField, Header("ìGÇÃà íuÇ…çáÇÌÇπÇΩXé≤ÇÃêßå¿")]
    private float fixedX;


    private void Awake()
    {
        cameraShake = GetComponent<CameraShake>();
    }

    private void LateUpdate()
    {
        if (target != null && !cameraShake.isHit)
        {
            // Xç¿ïWÇæÇØClampÇ∑ÇÈ
            float clampedX = Mathf.Clamp(target.position.x, -10f, fixedX);

            Vector3 newPosition = new Vector3(clampedX, fixedY, offsetZ);
            transform.position = newPosition + offsetX;
        }
    }
}