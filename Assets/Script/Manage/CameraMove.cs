using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Header("Player Object")]
    private GameObject player;

    [SerializeField, Header("Enemy Script")]
    private Enemy enemy;

    [SerializeField, Header("Black Object")]
    private GameObject black;

    [SerializeField, Header("Heart Canvas")]
    private GameObject Heart;

    [SerializeField, Header("MiniMap Canvas")]
    private GameObject Map;

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
        enemy.enabled = false;
        StartCoroutine(StartCamera());
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
                if (player != null && !cameraShake.isHit)
                {
                    float clampedX = Mathf.Clamp(player.transform.position.x, -10f, fixedX);
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

    private IEnumerator StartCamera()
    {
        // ç≈èâÇÕí«îˆèÛë‘Ç≈1ïbë“Ç¬
        state = CameraState.Following;
        yield return new WaitForSeconds(1f);
        black.GetComponent<Animator>().enabled = true;
        player.GetComponent<Player>().Rock = true;

        // ÉSÅ[ÉãÇ…å¸Ç©Ç§
        state = CameraState.MovingToGoal;
        // ÉSÅ[ÉãÇ…íÖÇ≠Ç‹Ç≈ë“Ç¬
        yield return new WaitUntil(() => Vector3.Distance(transform.position, StartOffset) < 0.05f);

        yield return new WaitForSeconds(1f);

        // ñﬂÇÈà íuÇï€ë∂
        float clampedX = Mathf.Clamp(player.transform.position.x, -10f, fixedX);
        returnPosition = new Vector3(clampedX, fixedY, offsetZ) + offsetX;

        black.GetComponent<Animator>().SetBool("ON", true);

        // ñﬂÇÈ
        state = CameraState.Returning;
        yield return new WaitUntil(() => Vector3.Distance(transform.position, returnPosition) < 0.05f);

        Heart.GetComponent<Animator>().enabled = true;
        Map.GetComponent<Animator>().enabled = true;

        // í«îˆçƒäJ
        state = CameraState.Following;
        player.GetComponent<Player>().Rock = false;
        player.GetComponent<Player>().JumpRock = false;
        enemy.enabled = true;

        yield return new WaitForSeconds(1f);
        Map.GetComponent<Animator>().enabled = false;
        Heart.GetComponent<Animator>().enabled = false;
    }
}
