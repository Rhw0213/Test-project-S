using UnityEngine;

public class BackgroundPart : MonoBehaviour
{
    [SerializeField] 
    private const float overlap = 19.2f;
    private Vector3 jumpSize;
    private float fixPositionY = 0;
    private Transform playerTransfrom; 

    private void Awake()
    {
        playerTransfrom = GetComponentInParent<Background>().player.transform; 
        jumpSize = new Vector3(overlap, 0, 0);
        fixPositionY = transform.position.y;
    }

    public void FixedUpdate()
    {
        int playerIndex = (int)(playerTransfrom.position.x / (overlap - 0.2f));

        playerIndex = playerTransfrom.position.x < 0 ? playerIndex - 1 : playerIndex;

        int backgroundIndex = (int)(transform.position.x / (overlap - 0.2f));

        Vector3 move;
        if (backgroundIndex == playerIndex - 1)
        {
            move = jumpSize * (playerIndex + 1);
            move.y = fixPositionY;
            transform.position = move;
        }
        else if (backgroundIndex == playerIndex + 2)
        {
            move = jumpSize * playerIndex;
            move.y = fixPositionY;
            transform.position = move;
        }
    }
}
