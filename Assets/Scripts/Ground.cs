using Unity.Burst.Intrinsics;
using UnityEngine;
using Assets.Scripts;
using NUnit.Framework;
using System.Collections.Generic;

public class Ground : MonoBehaviour
{
    public GameObject groundCenter;

    [SerializeField]
    private float positionY = -2.0f;

    List<Object> centerObjects = new List<Object>();
    void Start()
    {
        for (uint i = 0; i < 10; i++)
        {
            centerObjects.Add(
                Instantiate(groundCenter, new Vector3(-10f + i, positionY, 0f), Quaternion.identity, transform)
                );
            centerObjects.Add(
                Instantiate(groundCenter, new Vector3(i, positionY, 0f), Quaternion.identity, transform)
                );
        }
    }
}
