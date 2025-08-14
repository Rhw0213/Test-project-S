using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class DamagePopUpList : MonoBehaviour
{
    private List<GameObject> damagePopUps = new List<GameObject>();
    private const int count = 10;
    private int ativeIndex = 0;
    private GameObject damagePopUpPrefab;
    private void Start()
    {
        damagePopUpPrefab = Resources.Load<GameObject>("Prefabs/DamagePopUp");

        if (damagePopUpPrefab == null)
        {
            Debug.LogError("DamagePopUp prefab not found in Resources folder.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject damagePopUp = Instantiate(damagePopUpPrefab, transform);
            damagePopUp.SetActive(false);
            damagePopUps.Add(damagePopUp);
        }
    }
    public void OnPopUp(string damage)
    {
        for (int i = 0; i < damagePopUps.Count; i++)
        {
            if (!damagePopUps[i].activeSelf)
            {
                ativeIndex = i;
                damagePopUps[ativeIndex].SetActive(true);
                damagePopUps[ativeIndex].GetComponent<TextMeshPro>().text = damage;
                break;
            }
        }

        if (ativeIndex >= count)
        {
            ativeIndex = 0;
        }
    }
    public void  OnEndAnimation()
    {
        gameObject.SetActive(false);
    }
}
