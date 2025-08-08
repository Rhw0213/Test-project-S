using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    private Dictionary<string, GameObject> objects= new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> GetObjects()
    {
        return objects;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RegisterGameObject(GameObject obj, string name)
    {
        if (obj != null && !objects.ContainsKey(name))
        {
            objects[name] = obj;
            Debug.Log($"Registered GameObject: {name}");
        }
    }

    public void UnregisterGameObject(string name)
    {
        if (objects.ContainsKey(name))
        {
            objects.Remove(name);
            Debug.Log($"Unregistered GameObject: {name}");
        }
    }
}
