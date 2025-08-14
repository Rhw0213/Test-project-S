using UnityEngine;
using System.Collections.Generic;

public class SkillList : MonoBehaviour
{
    private Dictionary<string, GameObject> skillList = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> SkillLists => skillList;

    void Start()
    {
        GameObject[] skillPrefabs = Resources.LoadAll<GameObject>("Prefabs/Skill");

        foreach (var obj in skillPrefabs)
        {
            if (obj.gameObject.CompareTag("Skill"))
            {
                GameObject skillObj = obj.gameObject;
                skillList.Add(skillObj.name, Instantiate(obj, transform));
            }
        }
    }

    public string GetSkillName(string tag)
    {
        if (skillList.ContainsKey(tag))
        {
            return skillList[tag].name;
        }

        return null;
    }
}
