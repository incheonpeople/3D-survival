using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagalbe
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagalbe
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina {  get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;

    public event Action onTakeDamage;

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        if(hunger.curValue == 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }
        if(health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    public void Die()
    {
        Debug.Log("Die");
    }

    public void TakePhysicalDamage(int damage) // 횃불에 닿았을 때 데미지입는 UI 관련
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}
