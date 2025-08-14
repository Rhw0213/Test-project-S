using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 velocity;
    public Vector3 Velocity => velocity;

    public GameObject player;

    private Vector3 lastPosition;
    private Vector3 currentVelocity;

    [SerializeField] 
    private float cameraMoveSpeed = 4.0f; // 카메라 이동 속도


    private void Start()
    {
        Application.targetFrameRate = 60; // 원하는 FPS
        QualitySettings.vSyncCount = 0;   // V-Sync 끄기
    }

    private void Update()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime; 
        lastPosition = transform.position;
    }

    void FixedUpdate()  
    {
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;
            targetPosition.y = transform.position.y;
            targetPosition.z = transform.position.z;

            // 픽셀 퍼펙트 적용 (X, Y 둘 다)
            {
                float pixelSize = 1f / 16.0f;
                targetPosition.x = Mathf.Round(targetPosition.x / pixelSize) * pixelSize;
                targetPosition.y = Mathf.Round(targetPosition.y / pixelSize) * pixelSize;
            }

            // SmoothDamp로 부드럽게 이동
            Vector3 smoothPosition = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref currentVelocity,
                smoothTime : 0.1f // 부드럽게 이동하는 시간
            );

            transform.position = smoothPosition;
        }
    }
}
