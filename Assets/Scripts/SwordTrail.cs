using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SwordTrail : MonoBehaviour
{
    public List<Transform> trailPoints; 
    private int trailStartIndex = 0;
    private int trailEndIndex = 1;
    private Vector3 direction;
    bool isCalulateDirection = false;

    void Awake()
    {
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, trailPoints[trailEndIndex].position) > 0.1f)
        {
            if (!isCalulateDirection)
            {
                transform.SetParent(null);
                direction = (trailPoints[trailEndIndex].position - trailPoints[trailStartIndex].position).normalized;
                isCalulateDirection = true;
            }

            transform.position = Vector2.MoveTowards(transform.position, trailPoints[trailEndIndex].position, Time.fixedDeltaTime * 10f);
            //transform.position += direction * Time.fixedDeltaTime * 10.0f;

            if (trailEndIndex >= trailPoints.Count - 1)
            {
                gameObject.SetActive(false); 
                trailEndIndex = 1; 
                trailStartIndex = 0; 
                transform.position = trailPoints[0].position;
            }
        }
        else
        {
            Debug.Log(Vector2.Distance(transform.position, trailPoints[trailEndIndex].position));
            isCalulateDirection = false; 
            if (trailEndIndex < trailPoints.Count - 1)
            {
                trailStartIndex++;
                trailEndIndex++;
            }
        }
    }

    void OnEnable()
    {
        trailEndIndex = 1; 
        trailStartIndex = 0; 
    }
}
