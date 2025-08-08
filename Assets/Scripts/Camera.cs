using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            playerPosition.y = transform.position.y; 
            playerPosition.z = transform.position.z; 
            transform.position = playerPosition;
        }
    }
}
