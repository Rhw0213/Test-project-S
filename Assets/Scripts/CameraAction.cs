using UnityEngine;

public class CameraAction : MonoBehaviour
{
    public CameraController mainCamera;
    public Transform startPos;
    public Transform endPos;
    float floatZ;
    void Awake()
    {
        floatZ = mainCamera.transform.position.z;
        
    }
    void Start()
    {
        mainCamera.transform.position = startPos.position; 
    }
    void LateUpdate()
    {
        Vector3 move = Vector3.MoveTowards(mainCamera.transform.position, endPos.position, Time.deltaTime * 10.0f);
        move.z = floatZ; 
    }
}
