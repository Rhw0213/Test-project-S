using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 direction;

    public GameObject owner;

    [SerializeField]
    private float speed = 20f;

    [Header("플레이어로부터 얼마나 떨어져서 쏠지?")]
    public Vector3 offset;

    private Transform playerTransform;

    void Start()
    {
    }
    void OnEnable()
    {
        playerTransform = transform.root;
        direction = playerTransform.gameObject.GetComponent<SpriteRenderer>().flipX ? Vector3.left : Vector3.right;
        transform.position = playerTransform.position + offset; 

        Debug.Log($"Arrow enabled. Direction: {playerTransform.position}, {offset}");
    }

    void FixedUpdate()
    {
        transform.position += (direction * speed * Time.fixedDeltaTime);

        if (playerTransform.position.x < transform.position.x - 10f || 
            playerTransform.position.x > transform.position.x + 10f || 
            playerTransform.position.y < transform.position.y - 10f || 
            playerTransform.position.y > transform.position.y + 10f)
        {
            gameObject.SetActive(false); 
        }
    }
}
