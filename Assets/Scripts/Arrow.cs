using Assets.Scripts;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 direction;

    private GameObject arrowManager;

    [SerializeField]
    private float speed = 20f;

    [Header("플레이어로부터 얼마나 떨어져서 쏠지?")]
    public Vector3 offset;

    private GameObject owner;
    public GameObject Owner => owner;

    void Awake()
    {
        owner = MyCommon.FindParentPlayerAndEnemy(gameObject);
        if (owner == null)
        {
            Debug.LogError("Arrow Owner not found for " + gameObject.name);
            Debug.LogError("Arrow Make sure the owner has the Player or Enemy tag.");
            return;
        }

        arrowManager = GetComponentInParent<ArrowManager>().gameObject;
        if (arrowManager == null)
        {
            Debug.LogError("Player not found in the scene.");
        }
    }
    void OnEnable()
    {
        bool isFlipX = owner.GetComponent<SpriteRenderer>().flipX;
        direction =  isFlipX ? Vector3.left : Vector3.right;
        gameObject.GetComponent<SpriteRenderer>().flipX = isFlipX;

        Vector3 worldPos = owner.transform.position + offset; 

        transform.parent = null;     
        transform.position = worldPos;
    }

    void FixedUpdate()
    {
        transform.position += (direction * speed * Time.fixedDeltaTime);

        if (owner.transform.position.x < transform.position.x - 10f || 
            owner.transform.position.x > transform.position.x + 10f || 
            owner.transform.position.y < transform.position.y - 10f || 
            owner.transform.position.y > transform.position.y + 10f)
        {
            transform.parent = arrowManager.transform;
            gameObject.SetActive(false); 
        }
    }
}
