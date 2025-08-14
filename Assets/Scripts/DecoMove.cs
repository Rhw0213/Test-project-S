using TMPro;
using UnityEngine;

public class DecoMove : MonoBehaviour
{
    private CameraController mainCamera;
    private Transform cameraTransform;
    private CharactorBehaviour player;
    private float imageSizeHalfX;
    private float imageSizeX;

    [SerializeField]
    private float moveRatio = 0.8f; 

    void Awake()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().gameObject.GetComponent<Transform>();
        player = GetComponentInParent<Background>().player.GetComponent<CharactorBehaviour>();
        imageSizeX = GetComponent<Renderer>().bounds.size.x - 0.2f;
        imageSizeHalfX = imageSizeX * 0.5f;
    }

    private void Update()
    {
    }

    void FixedUpdate()
    {

        if (player.Move.magnitude > 0)
        {
            transform.position += player.Move * moveRatio * Time.deltaTime;
        }
        //float cameraPositionX = cameraTransform.position.x;

        //Vector3 move = cameraTransform.position * moveRatio;
        //move.y = 0;
        //move.z = 0;

        //if (transform.position.x > cameraPositionX - imageSizeHalfX &&
        //    transform.position.x < cameraPositionX + imageSizeHalfX)
        //{
        //    move.x = cameraPositionX;
        //}
        //if (transform.position.x <= cameraPositionX - imageSizeHalfX)
        //{
        //    move.x -= imageSizeX;
        //}
        //else if (transform.position.x >= cameraPositionX + imageSizeHalfX) 
        //{
        //    move.x += imageSizeX ;
        //}

        //    transform.position = move;
    }
}
