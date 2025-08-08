using UnityEngine;

public class Attack : MonoBehaviour
{
    private SpriteRenderer ownerSpriteRenderer;
    [SerializeField]
    private uint attackDamage = 10;

    public GameObject swordTrail;
    private void Awake()
    {
        ownerSpriteRenderer = transform.root.gameObject.GetComponent<SpriteRenderer>();
        gameObject.SetActive(false); 
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))  
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.SetDamage(attackDamage, ownerSpriteRenderer.flipX);

        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.SetDamage(attackDamage);
        }
        
        if (gameObject.CompareTag("Arrow"))
        {
            gameObject.SetActive(false);  
        }
    }

    public void OnEnable()
    {
        if (swordTrail != null)
        {
            swordTrail.SetActive(true); 
        }
    }
}

