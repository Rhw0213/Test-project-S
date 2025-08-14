using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    private Rigidbody2D myRegidbody;
    private void Awake()
    {
        myRegidbody = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy")) &&
            collision.gameObject.CompareTag("Ground"))
        {
            myRegidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            return;
        }
    }
}
