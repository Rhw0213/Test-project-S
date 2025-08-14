using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

class CharactorBehaviour : MonoBehaviour
{
    //private Vector3 velocity;
    //public Vector3 Velocity => velocity;

    private Vector3 move; 
    public Vector3 Move => move;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("이동 속도")]
    [SerializeField]
    private float normalSpeed = 1.5f;

    public float NormalSpeed => normalSpeed;

    [Header("점프 힘")]
    [SerializeField]
    private float jumpForce = 4.0f;

    private Action<string> aniAttackEvent;
    private Action<float> aniMoveEvent;

    private MyCharacter character;
    private Vector3 lastPosition;
    private Status status;

    void Awake()
    {
        rb              = GetComponent<Rigidbody2D>();
        spriteRenderer  = GetComponent<SpriteRenderer>();
        character       = GetComponent<MyCharacter>();
        status          = GetComponent<Status>();
        aniAttackEvent  = GetComponent<MyAnimation>().OnAnimationAttack;
        aniMoveEvent    = GetComponent<MyAnimation>().OnAnimationMove;
    }

    void FixedUpdate()
    {
        transform.position += move * normalSpeed * Time.fixedDeltaTime;

        Debug.Log($"Move: {move}");

        //이것도 떨림
        //Vector3 pos = rb.position;
        //pos = pos + (move * normalSpeed * Time.fixedDeltaTime);

        //rb.MovePosition(pos);
    }

    public void MoveFunc(Vector2 move)
    {
        if (status.CurrentHp <= 0)
        {
            this.move = Vector3.zero; 
            return;
        }

        move.y = 0; 
        this.move = move;

        aniMoveEvent?.Invoke(move.x);

        // 방향 전환
        if (move.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (move.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void OnMove(Vector2 move)
    {
        MoveFunc(move);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        //if (value.y > 0) return; 

        MoveFunc(context.ReadValue<Vector2>());
    }

    private void JumpFunc(bool isJump)
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.05f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    public void OnJump(bool isJump)
    {
         JumpFunc(isJump);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpFunc(true);
        }
    }
    private void AttackFunc(bool isAttack)
    {
        const string name = "attack1";

        if (isAttack)
        {
            aniAttackEvent?.Invoke(name);
            character.SkillRef.SkillLists[name].SetActive(true);
        }
        else 
        {
            character.SkillRef.SkillLists[name].SetActive(false);
        }
    }
    public void OnAttack(bool isAttack)
    {
        AttackFunc(isAttack);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackFunc(true);
        }
        else if (context.canceled)
        {
            AttackFunc(false);
        }
    }

    private void FireFunc(bool isFire)
    {
        const string name = "attack3";

        if (isFire)
        {
            aniAttackEvent?.Invoke(name);
        }
    }

    public void OnFire(bool isFire)
    {
        FireFunc(isFire);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        FireFunc(context.started);
    }
}
