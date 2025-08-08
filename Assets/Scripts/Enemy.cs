using UnityEngine;
using Assets.Scripts;
using UnityEngine.InputSystem;

public class Enemy: MonoBehaviour
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

    public GameObject attack1Object;
    public GameObject attack2Object;
    private Vector3 leftScale = new Vector3(-1, 1, 1);
    private Vector3 rightScale = new Vector3(1, 1, 1);
    private Vector3 leftHurt = new Vector3(-300f, 0, 0);
    private Vector3 rightHurt = new Vector3(300f, 0, 0);

    private PolygonCollider2D attack1Collider;
    private PolygonCollider2D attack2Collider;

    private BoxCollider2D playerCollider;
    private Vector2 colliderRightOffset;
    private Vector2 colliderLeftOffset;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();

        colliderRightOffset = playerCollider.offset;
        colliderLeftOffset = new Vector2(0.11f, playerCollider.offset.y);

        attack1Collider = attack1Object.GetComponent<PolygonCollider2D>();
        attack2Collider = attack2Object.GetComponent<PolygonCollider2D>();
        attack1Collider.enabled = false;
        attack2Collider.enabled = false;
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
        if (move.magnitude > 0)
        {
            //transform.position += move * speed * Time.fixedDeltaTime;
        }

        Animation(move);
    }

    public void SetDamage(uint damage, bool rightDirection = true)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
        {
            HP -= damage;

            animator.SetTrigger("hurt");

            if (rightDirection)
            {
                //transform.position += rightHurt * Time.fixedDeltaTime;
                rb.AddForce(leftHurt, ForceMode2D.Force);
                spriteRenderer.flipX = false;
                playerCollider.offset = colliderRightOffset;
            }
            else
            {
                rb.AddForce(rightHurt, ForceMode2D.Force);
                spriteRenderer.flipX = true;
                playerCollider.offset = colliderLeftOffset;
            }
        }
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

    public void OnMove(Vector2 value)
    {
        move = value;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
        }
    }

    public void OnAttack(bool triger)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            if (triger)
            {
                animator.SetTrigger("attack1");
                attack1Collider.enabled = true;
                SetAttackColliderFlibX(attack1Object);
            }
        }
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
        attack1Collider.enabled = false;

        animator.SetTrigger("attack2");
        attack2Collider.enabled = true;

        SetAttackColliderFlibX(attack2Object);
    }

    public void OnEndAttack2()
    {
        attack2Collider.enabled = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with player.");
        }

    }
}
