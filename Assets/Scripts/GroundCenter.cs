using UnityEngine;

public class GroundCenter : MonoBehaviour
{
    void Start()
    {
        ObjectManager.instance.RegisterGameObject(gameObject, "GroundCenter");
    }

}
