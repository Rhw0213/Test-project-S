using UnityEngine;

public class DamagePopUp : MonoBehaviour 
{
    public void OnDisableGameObject()
    {
        gameObject.SetActive(false);
    }
}
