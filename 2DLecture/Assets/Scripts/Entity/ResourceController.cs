using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 1.0f;

    private BaseControllor baseControllor;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.Health;

    public AudioClip damageClip;

    private Action<float, float> OnChangeHealth;
    private void Awake()
    {
        baseControllor = GetComponent<BaseControllor>();
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        CurrentHealth = statHandler.Health;
    }

    private void Update()
    {
        if(timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if(timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if(change < 0 && timeSinceLastChange < healthChangeDelay) //������ ���� �������޴°Ŵϱ� ����� ���� ��ȭ�����ص� ���� ������?
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
        
        if(change < 0)
        {
            animationHandler.Damage();

            if (damageClip != null) SoundManager.PlayClip(damageClip);
        }

        if(CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        baseControllor.Death();
    }

    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }
}
