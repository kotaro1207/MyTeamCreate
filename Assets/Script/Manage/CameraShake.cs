using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField, Header("振動する時間")]
    private float _shakeTime;
    [SerializeField, Header("振動する大きさ")]
    private float _shakeMagnitude;

    public bool isHit { get; private set; } = false;

    [SerializeField, Header("Player Transform")]
    private Transform target;

    private float _shakeCount = 0f;

    private float Xpos => target.transform.position.x >= 23.7f ? 0 : 8f;


    //private void Update()
    //{
    //    if ()
    //    {
    //        Xpos = 0;
    //    }
    //    else
    //    {
    //        Xpos = 8f;
    //    }
    //}

    public void _ShakeCheck()
    {
        StartCoroutine(_Shake());
    }

    IEnumerator _Shake()
    {
        isHit = true;
        Vector3 initPos = target.transform.position;

        while (_shakeCount < _shakeTime)
        {
            if (target.transform.position.x >= 23.7f)
            {
                initPos.x = 31.7f;
            }

            initPos.y = 4f;

            float x = initPos.x + Random.Range(-_shakeMagnitude, _shakeMagnitude);
            float y = initPos.y + Random.Range(-_shakeMagnitude, _shakeMagnitude);

            transform.position = new Vector3(x + Xpos, y, -10);

            _shakeCount += Time.deltaTime;

            yield return null;
        }
        transform.position = target.transform.position;
        _shakeCount = 0f;
        isHit = false;
    }
}