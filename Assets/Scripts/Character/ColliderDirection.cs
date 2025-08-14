using Assets.Scripts;
using UnityEngine;

public class ColliderDirection : MonoBehaviour
{
    private Collider2D myCollider;
    private SpriteRenderer ownerSpriteRenderer;

    [Header("캐릭터가 왼쪽으로 움직일때 콜라이더 위치 이동")]
    [SerializeField]
    private Vector3 characterColliderLeftOffset;

    private Vector3 characterColliderRightOffset;

    private Vector3 leftScale = new Vector3(-1, 1, 1);
    private Vector3 rightScale = new Vector3(1, 1, 1);


    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        ownerSpriteRenderer = MyCommon.FindParentPlayerAndEnemy(gameObject).GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        characterColliderRightOffset = myCollider.offset;
    }

    public void FixedUpdate()
    {
        SetAttackColliderFlibX();
    }

    public void SetAttackColliderFlibX()
    {
        if (gameObject.CompareTag("Skill"))
        {
            if (ownerSpriteRenderer.flipX)
            {
                transform.localScale = leftScale;
            }
            else
            {
                transform.localScale = rightScale;
            }
        }
        else if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy"))
        {
            if (ownerSpriteRenderer.flipX)
            {
                myCollider.offset = characterColliderLeftOffset;
            }
            else
            {
                myCollider.offset = characterColliderRightOffset;
            }
        }
        else
        {
            Debug.LogWarning("Unknown tag: " + gameObject.tag);
        }
    }

}
