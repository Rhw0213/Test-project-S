using UnityEngine;

class MyCharacter : MonoBehaviour
{
    private SkillList skillRef;
    public SkillList SkillRef => skillRef;

    private void Awake()
    {
        skillRef = GetComponentInChildren<SkillList>();
        if (skillRef == null)
        {
            Debug.LogError("Skill component not found in children.");
        }
    }
}
