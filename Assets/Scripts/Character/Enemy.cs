using System;
using UnityEngine;
using UnityEngine.InputSystem;

class Enemy : MonoBehaviour
{
    public Transform player;

    Action<Vector2> moveEvent;
    Action<bool> attackEvent;
    void Awake()
    {
        moveEvent = GetComponent<CharactorBehaviour>().OnMove;
        attackEvent = GetComponent<CharactorBehaviour>().OnAttack; 
        player = Resources.Load<GameObject>("Prefabs/Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        direction.y = 0; 
        direction.x = Mathf.Sign(direction.x); 

        moveEvent?.Invoke(direction);
    }
}
