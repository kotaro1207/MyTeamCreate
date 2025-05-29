using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GunMove : MonoBehaviour
{
    /** 追尾対象 */
    [SerializeField] private Transform target;
    /** 回転速度 */
    [SerializeField] private float rotationSpeed;
    /** 画像の正面向き */
    [SerializeField] private Vector3 fromDirection;

    public float recoilAngle = -10f;
    public float recoilSpeed = 20f;

    private bool isRecoiling = false;

    void Update()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Vector3 heading = target.position - transform.position;

        Quaternion targetRotation = Quaternion.FromToRotation(fromDirection, heading.normalized);

        Vector3 targetEulerAngles = targetRotation.eulerAngles;

        // Z軸の角度は0〜360度なので、-180〜180度に直す
        float z = targetEulerAngles.z;
        if (z > 180f) z -= 360f;

        // -30〜30度に制限
        z = Mathf.Clamp(z, -30f, 30f);

        targetEulerAngles.z = z;
        targetRotation = Quaternion.Euler(targetEulerAngles);

        //targetRotation = Mathf.Clamp(targetRotation, -30,30);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    public void Recoil()
    {
        if (!isRecoiling)
        {
            StartCoroutine(RecoilRotation());
        }
    }

    private IEnumerator RecoilRotation()
    {
        isRecoiling = true;

        ///float Angle = transform.rotation + Quaternion.Euler(0,0,recoilAngle);

        transform.position = new Vector3(39f, 3.11377f, 0);

        Vector2 startTr = new Vector3(39f, 3.11377f, 0);
        Vector2 endTr = new Vector3(39.2f, 3.11377f, 0);

        float Angle = (transform.rotation * Quaternion.Euler(0, 0, recoilAngle)).eulerAngles.z;

        //Angle = Mathf.Clamp(Angle, -30, 30);

        Quaternion startRot = Quaternion.Euler(0, 0, 0);
        Quaternion recoilRot = Quaternion.Euler(0, 0, Angle);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * recoilSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, recoilRot, t);

            transform.position = Vector3.Lerp(startTr, endTr, t);

            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * recoilSpeed;
            transform.rotation = Quaternion.Slerp(recoilRot, transform.rotation, t);

            transform.position = Vector3.Lerp(endTr, startTr, t);

            yield return null;
        }

        //transform.rotation = transform.rotation;

        transform.position = startTr;

        isRecoiling = false;
    }
}
