using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCameraMove : MonoBehaviour
{
    [SerializeField, Header("PlayerÇÃTransform")]
    private GameObject player;

    private Transform target;

    [SerializeField, Header("Ç∏ÇÁÇµ")]
    private Vector3 offsetX = new Vector3(7, 0, -10);

    [SerializeField, Header("ÉSÅ[Éãà íu")]
    private Vector3 StartOffset = new Vector3(32, 0, -10);

    private CameraShake cameraShake;
    private float fixedY = 0f;
    private float offsetZ = -10f;

    [SerializeField, Header("ìGÇÃà íuÇ…çáÇÌÇπÇΩXé≤ÇÃêßå¿")]
    private float fixedX;

    private enum CameraState { Following, MovingToGoal, Returning }
    private CameraState state = CameraState.Following;

    private float moveSpeed = 10f;
    private Vector3 returnPosition;

    private void Start()
    {
        target = player.transform;
        //StartCoroutine(YadotsumuriSpeak());
    }

    private void Awake()
    {
        cameraShake = GetComponent<CameraShake>();
    }

    private void LateUpdate()
    {
        switch (state)
        {
            case CameraState.Following:
                if (target != null && !cameraShake.isHit)
                {
                    float clampedX = Mathf.Clamp(target.position.x, -10f, fixedX);
                    Vector3 newPosition = new Vector3(clampedX, fixedY, offsetZ);
                    transform.position = newPosition + offsetX;
                }
                break;

            case CameraState.MovingToGoal:
                transform.position = Vector3.MoveTowards(transform.position, StartOffset, moveSpeed * Time.deltaTime);
                break;

            case CameraState.Returning:
                transform.position = Vector3.MoveTowards(transform.position, returnPosition, moveSpeed * Time.deltaTime);
                break;
        }
    }

    public void CameraGoalFollow()
    {
        StartCoroutine(StartCamera());
    }
    private IEnumerator StartCamera()
    {
        // ç≈èâÇÕí«îˆèÛë‘Ç≈1ïbë“Ç¬
        state = CameraState.Following;
        yield return new WaitForSeconds(1f);
        player.GetComponent<TutorialPlayer>().moveRock = true;
        // ÉSÅ[ÉãÇ…å¸Ç©Ç§
        state = CameraState.MovingToGoal;
        // ÉSÅ[ÉãÇ…íÖÇ≠Ç‹Ç≈ë“Ç¬
        yield return new WaitUntil(() => Vector3.Distance(transform.position, StartOffset) < 0.05f);

        yield return new WaitForSeconds(1f);

        // ñﬂÇÈà íuÇï€ë∂
        float clampedX = Mathf.Clamp(target.position.x, -10f, fixedX);
        returnPosition = new Vector3(clampedX, fixedY, offsetZ) + offsetX;
        player.GetComponent<TutorialPlayer>().moveRock = false;

        // ñﬂÇÈ
        state = CameraState.Returning;
        yield return new WaitUntil(() => Vector3.Distance(transform.position, returnPosition) < 0.05f);

        // í«îˆçƒäJ
        state = CameraState.Following;
    }
}
