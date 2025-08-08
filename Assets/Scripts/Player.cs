using UnityEngine;
using Assets.Scripts;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private uint HP = 100;

    private Vector3 move;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [SerializeField]
    private float nomalSpeed = 1.5f;
    [SerializeField]
    private float runSpeed = 3.5f;

    private float speed = 0;

    [Header("Á¡ÇÁ Èû")]
    [SerializeField]
    private float jumpForce = 4.0f;

    public GameObject attack1Object;
    public GameObject attack2Object;
    public GameObject attack3Object;

    private Vector3 leftScale = new Vector3(-1, 1, 1);
    private Vector3 rightScale = new Vector3(1, 1, 1);

    private PolygonCollider2D attack1Collider;
    private PolygonCollider2D attack2Collider;

    private BoxCollider2D playerCollider;
    private Vector2 colliderRightOffset;
    private Vector2 colliderLeftOffset;

    public Vector2 Move
    {
        get { return move; }
    }

    private void Awake()
    {
        animator        = GetComponent<Animator>();
        spriteRenderer  = GetComponent<SpriteRenderer>();
        rb              = GetComponent<Rigidbody2D>();
        playerCollider  = GetComponent<BoxCollider2D>();

        colliderRightOffset = playerCollider.offset;
        colliderLeftOffset = new Vector2 (0.11f, playerCollider.offset.y); 

        attack1Collider = attack1Object.GetComponent<PolygonCollider2D>();
        attack2Collider = attack2Object.GetComponent<PolygonCollider2D>();
    }

    void Start()
    {
        speed = nomalSpeed; 
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        transform.position += move * speed * Time.fixedDeltaTime;
        
        Animation(move);
    }
    public void SetDamage(uint damage)
    {
        HP -= damage;
    }

    private void Animation(Vector2 move)
    {
        animator.SetFloat("walk", move.magnitude);

        // ÁÂ¿ì ÀüÈ¯
        if (move.x > 0)
        {
            spriteRenderer.flipX = false;
            playerCollider.offset = colliderRightOffset;
        }
        else if (move.x < 0)
        {
            spriteRenderer.flipX = true;
            playerCollider.offset = colliderLeftOffset;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        if (value.y > 0) return; 

        move = context.ReadValue<Vector2>(); 
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") || 
            animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            if (context.started)
            {
                animator.SetTrigger("attack1");
                attack1Object.SetActive(true);
                SetAttackColliderFlibX(attack1Object);
            }
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") || 
            animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            if (context.started)
            {
                animator.SetTrigger("attack3");
                attack3Object.SetActive(true);
                SetAttackColliderFlibX(attack3Object);
            }
        }
    }

    public void Fire()
    {
        attack3Object.GetComponent<ArrowManager>().Fire();
    }

    public void SetAttackColliderFlibX(GameObject obj)
    {
        if (spriteRenderer.flipX)
        {
            obj.transform.localScale = leftScale;
        }
        else
        {
            obj.transform.localScale = rightScale;
        }
    }

    public void OnEndAttack1()
    {
        attack1Object.SetActive(false);

        animator.SetTrigger("attack2");
        attack2Object.SetActive(true);

        SetAttackColliderFlibX(attack2Object);
    }

    public void OnEndAttack2()
    {
        attack2Object.SetActive(false);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetBool("run", true);
            speed = runSpeed; 
        }
        else if (context.canceled)
        {
            animator.SetBool("run", false);
            speed = nomalSpeed; 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Player has collided with the ground.");
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Player is still colliding with the ground.");
        }
    }
}
