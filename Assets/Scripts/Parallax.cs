using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    private CharactorBehaviour target;

    Material mat;
    float distance;

    float posY;
    float posZ;

    [Range(0.0f, 1.0f)]
    public float ratio = 0.5f;

    void Awake()
    {
        target = GetComponentInParent<Background>().player.GetComponent<CharactorBehaviour>();
        Debug.Log(target.name);

        mat = GetComponent<Renderer>().material; 
        posY = transform.position.y;
        posZ = transform.position.z;
    }

    void FixedUpdate()
    {
        CalculateDirection(out float sign);

        distance += Time.deltaTime * sign * ratio * 0.2f;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);

        Vector3 move = target.transform.position;
        move.y = posY; 
        move.z = posZ;
        transform.position = move;
    }

    void CalculateDirection(out float sign)
    {
        if (target.Move.x > 0) { sign = 1; }
        else if (target.Move.x < 0) { sign = -1; }
        else { sign = 0; }
    }
}
