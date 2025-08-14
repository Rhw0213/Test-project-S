using UnityEngine;

public class Skill : MonoBehaviour
{
    [Header("공격력")]
    [SerializeField]
    private float attackPower = 10f;
    public float AttackPower => attackPower;

    [Header("공격받을시 밀리는정도")]
    [SerializeField]
    private float pushValue = 100;
    public float PushValue => pushValue;
}
