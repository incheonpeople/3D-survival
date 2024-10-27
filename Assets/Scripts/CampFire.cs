using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage; // 데미지를 얼마나 줄건지, 얼마나 자주 줄건지
    public float damageRate;

    List<IDamagalbe> things = new List<IDamagalbe>();
    void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for(int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.TryGetComponent(out IDamagalbe damagaIbe))
        {
            things.Add(damagaIbe);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out IDamagalbe damagable))
        {
            things.Remove(damagable);
        }
    }
}
