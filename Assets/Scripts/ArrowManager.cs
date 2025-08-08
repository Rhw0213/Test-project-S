using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ArrowManager : MonoBehaviour
{
    public GameObject arrowPrefab;
    private List<GameObject> arrows = new List<GameObject>();
    private int arrowIndex = 0;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform); 
            arrow.SetActive(false); 
            arrows.Add(arrow);
        }
    }
    public void Fire()
    {
        if (!arrows[arrowIndex].activeSelf)
        {
            arrows[arrowIndex++].SetActive(true);

            if (arrowIndex >= arrows.Count)
            {
                arrowIndex = 0; 
            }
        }
    }

}
