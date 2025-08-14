using UnityEngine;
using UnityEngine.TextCore.Text;

public class MyAnimation : MonoBehaviour
{
    private Animator animator;

    private MyCharacter character;
    private void Awake()
    {
       animator = GetComponent<Animator>(); 
       character = GetComponentInParent<MyCharacter>();
    }
    public bool IsAnimaionStateDeath()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("death"); 
    }


    public void OnAnimationMove(float value)
    {
        animator.SetFloat("walk", Mathf.Abs(value));
    }

    public void OnAnimationAttack(string name)
    {
        if (animator != null)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            {
                animator.SetTrigger(name);
            }
        }
    }

    public void OnAnimationHurt()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            animator.SetTrigger("hurt");
        }
    }

    public void OnAnimationDeath()
    {
        animator.SetTrigger("death");
    }

    public void OnEndAnimationDeath()
    {
        gameObject.SetActive(false);
    }

    public void OnEndAttack1()
    {
        character.SkillRef.SkillLists["attack1"].SetActive(false);

        animator.SetTrigger("attack2");

        character.SkillRef.SkillLists["attack2"].SetActive(true);
    }

    public void OnEndAttack2()
    {
        character.SkillRef.SkillLists["attack2"].SetActive(false);
    }

    public void OnAttack3()
    {
        character.SkillRef.SkillLists["attack3"].GetComponent<ArrowManager>().Fire();
    }
}
