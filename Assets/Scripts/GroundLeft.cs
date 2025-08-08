using UnityEngine;

public class GroundLeft : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectManager.instance.RegisterGameObject(gameObject, "GroundLeft");
    }
}
