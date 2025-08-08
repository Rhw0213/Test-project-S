using UnityEngine;

public class GroundRight : MonoBehaviour
{
    void Start()
    {
        ObjectManager.instance.RegisterGameObject(gameObject, "GroundRight");
    }
}
