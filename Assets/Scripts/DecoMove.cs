using UnityEngine;

public class DecoMove : MonoBehaviour
{
    private Player player;

    void Awake()
    {
        player = GetComponentInParent<Background>().player.GetComponent<Player>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if (player.Move.magnitude > 0)
        {
            Vector3 move = player.Move * Time.fixedDeltaTime * 0.3f;
            move.y = 0;
            transform.position += move;
        }
    }
}
