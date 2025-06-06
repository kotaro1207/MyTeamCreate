using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCameraMove : MonoBehaviour
{
    [SerializeField, Header("PlayerのTransform")]
    private GameObject player;

    private Transform target;

    [SerializeField, Header("ずらし")]
    private Vector3 offsetX = new Vector3(7, 0, -10);

    [SerializeField, Header("ゴール位置")]
    private Vector3 StartOffset = new Vector3(32, 0, -10);

    private CameraShake cameraShake;
    private float fixedY = 0f;
    private float offsetZ = -10f;

    [SerializeField, Header("敵の位置に合わせたX軸の制限")]
    private float fixedX;

    [SerializeField] private TwoTextController textCon;
    public bool isPush { get; private set; } = false;
    public bool isFinished { get; private set; } = false;

    private enum CameraState { Following, MovingToGoal, Returning }
    private CameraState state = CameraState.Following;

    private float moveSpeed = 20f;
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

    private void Update()
    {
        if (textCon.currentSentenceNum == 2)
        {
            isPush = true;
        }
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
                transform.position = Vector3.MoveTowards(transform.position, returnPosition, moveSpeed + 0.25f * Time.deltaTime);
                break;
        }
    }

    public void CameraGoalFollow()
    {
        StartCoroutine(StartCamera());
    }
    private IEnumerator StartCamera()
    {
        // 最初は追尾状態で1秒待つ
        state = CameraState.Following;
        yield return new WaitForSeconds(1f);
        //player.GetComponent<TutorialPlayer>().moveRock = true;
        // ゴールに向かう
        state = CameraState.MovingToGoal;
        // ゴールに着くまで待つ
        yield return new WaitUntil(() => Vector3.Distance(transform.position, StartOffset) < 0.05f);
        isFinished = true;

        //yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => isPush == true);

        yield return new WaitForSeconds(1.25f);
        // 戻る位置を保存
        float clampedX = Mathf.Clamp(target.position.x, -10f, fixedX);
        returnPosition = new Vector3(clampedX, fixedY, offsetZ) + offsetX;
        //player.GetComponent<TutorialPlayer>().moveRock = false;

        // 戻る
        //state = CameraState.Returning;
        transform.position = returnPosition;
        yield return new WaitUntil(() => Vector3.Distance(transform.position, returnPosition) < 0.05f);

        // 追尾再開
        state = CameraState.Following;
    }
}
