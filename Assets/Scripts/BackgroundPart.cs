using UnityEngine;

public class BackgroundPart : MonoBehaviour
{
    private float       overlap;
    private Vector3     jumpSize;
    private float       fixPositionY;
    private Transform   playerTransfrom; 

    private void Awake()
    {
        playerTransfrom = GetComponentInParent<Background>().player.transform; 
        overlap = GetComponent<SpriteRenderer>().bounds.size.x - 0.1f;
        jumpSize = new Vector3(overlap, 0, 0);
        fixPositionY = transform.position.y;
    }

    public void FixedUpdate()
    {
        int playerIndex = (int)((playerTransfrom.position.x - (transform.position.x % overlap)) / overlap);

        playerIndex = playerTransfrom.position.x < 0 ? playerIndex - 1 : playerIndex;

        float offsetBackgroundPositionX = transform.position.x;
        int backgroundIndex = (int)(offsetBackgroundPositionX / overlap);

        Vector3 move;
        if (backgroundIndex == playerIndex - 1)
        {
            move = jumpSize * 2;
            move.y = fixPositionY;
            transform.position += move;
        }
        else if (backgroundIndex == playerIndex + 2)
        {
            move = jumpSize * 2;
            move.y = fixPositionY;
            transform.position -= move;
        }
    }
}
