using UnityEngine;
using Assets.Scripts;
public class AttackCollision : MonoBehaviour
{
    //private SpriteRenderer ownerSpriteRenderer;
    private GameObject owner;
    private Skill skill;

    private void Awake()
    {
        owner = MyCommon.FindParentPlayerAndEnemy(gameObject);
        if (owner == null)
        {
            Debug.LogError("Owner not found for " + gameObject.name);
            Debug.LogError("Make sure the owner has the Player or Enemy tag.");
            return;
        }
        skill = GetComponent<Skill>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (owner == null)
        {
            return;
        }

        if (owner.CompareTag(collision.gameObject.tag))
        {
            return; 
        }

        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {

            GameObject damageObject = collision.gameObject;
            MyAnimation myAni = damageObject.GetComponent<MyAnimation>();

            Status status = damageObject.GetComponent<Status>();

            if (status.CurrentHp <= 0) return; 

            status.DamageHp(skill.AttackPower);

            bool isFlipX = owner.GetComponent<SpriteRenderer>().flipX;
            myAni.OnAnimationHurt();
            MyCommon.ChangeFlibX(damageObject, !isFlipX);

            damageObject.GetComponent<Rigidbody2D>().AddForce((isFlipX ? Vector3.left : Vector3.right) * skill.PushValue);

            if (gameObject.CompareTag("Arrow"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
