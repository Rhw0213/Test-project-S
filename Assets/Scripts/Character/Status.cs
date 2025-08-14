using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class Status : MonoBehaviour
{
    private Image image;
    private CharactorBehaviour charactorBehaviour;
    private Action deathEvent;
    private Action<string> damagePopUpEvent;
    
    [SerializeField]
    private float maxHp = 100;

    public float MaxHp {
        get => maxHp;
        set { maxHp = value; }   
    } 

    [SerializeField]
    private float currentHp = 0;
    
    public float CurrentHp { 
        get => currentHp; 
        set 
        {
            if (value <= 0) 
            { 
                currentHp = 0;
                deathEvent?.Invoke();
            }
            else if (value > maxHp) { currentHp = maxHp; }
            else { currentHp = value; }
        } 
    }

    [SerializeField]
    private float maxMp = 100;
    public float MaxMp {
        get => maxMp;
        set { maxMp = value; }   
    } 

    [SerializeField]
    private float currentMp = 0;
    public float CurrentMp { 
        get => currentMp; 
        set 
        {
            if (value <= 0) { currentMp = 0; }
            else if (value > maxMp) { currentMp = maxMp; }
            else { currentMp = value; }
        } 
    }

    public void DamageHp(float amount) 
    {
        damagePopUpEvent?.Invoke(amount.ToString());
        CurrentHp -= amount; 
    }
    public void AddHp(float amount) { CurrentHp += amount; }
    public void UseMp(float amount) { CurrentMp -= amount; }
    public void AddMp(float amount) { CurrentMp += amount; }

    void Awake()
    { 
        image = MyCommon.FindChildTag(gameObject, "HpBar").GetComponent<Image>();
        deathEvent = GetComponent<MyAnimation>().OnAnimationDeath;
        charactorBehaviour = GetComponent<CharactorBehaviour>();

        currentHp = maxHp;
        currentMp = maxMp;
    }

    private void Start()
    {
        damagePopUpEvent = GetComponentInChildren<DamagePopUpList>().OnPopUp;
    }

    void FixedUpdate()
    {
        image.fillAmount = Mathf.Lerp(image.fillAmount, currentHp / maxHp, Time.fixedDeltaTime * 10.0f);
    }
}
