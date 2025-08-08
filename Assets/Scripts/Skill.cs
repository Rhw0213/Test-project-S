using UnityEditor.Animations;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private string skillName;

    public GameObject skillUI;
    public GameObject owner;

    private Animator animator;
    
    private void Awake()
    {
       
    }

    public void SetSkill(string skillName)
    {
        this.skillName = skillName;

        UseSkill();
    }

    public void UseSkill()
    {
        if (skillName.Equals("arrow"))
        {
            animator.SetTrigger("attack3");
        }
    }

}
