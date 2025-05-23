using System.Runtime.CompilerServices;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class StartCameraMove : MonoBehaviour
{
    enum CameraState { Following, MovingToGoal, Returning }
    CameraState currentState = CameraState.Following;

    public Transform player;
    public Transform goal;
    public float speed = 3f;

    private Vector3 originalFollowOffset;
    private Vector3 goalPosition;

    void Start()
    {
        originalFollowOffset = transform.position - player.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case CameraState.Following:
                transform.position = Vector3.Lerp(transform.position, player.position + originalFollowOffset, Time.deltaTime * speed);
                break;

            case CameraState.MovingToGoal:
                transform.position = Vector3.MoveTowards(transform.position, goal.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, goal.position) < 0.1f)
                {
                    currentState = CameraState.Returning;
                }
                break;

            case CameraState.Returning:
                Vector3 targetPos = player.position + originalFollowOffset;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                {
                    currentState = CameraState.Following;
                }
                break;
        }
    }
}
