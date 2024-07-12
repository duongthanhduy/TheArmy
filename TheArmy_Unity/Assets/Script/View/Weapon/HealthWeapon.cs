using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthWeapon : WeaponView
{
    private float timeTween1 = 0.2f, timeTween2 = 0.1f,timeTween3 = 0.1f;
    public override void CaculatorLimitFireRate()
    {
        base.CaculatorLimitFireRate();
        limitFireRate = timeTween1 + timeTween2 + timeTween3;
    }
    private void Awake()
    {
        transform.localPosition = new Vector3(0.2f, 0.2f, 0);
    }
    public override void SetWeaponView()
    {

    }

    public override void Attack()
    {
        transform.DOScale(Vector2.one *1.3f, timeTween1).OnComplete(()=> {
            Fire();
            transform.DOScale(Vector2.one, timeTween2).SetDelay(timeTween3);
        });
        
    }
    public override void Fire()
    {
        SpecialTarget.entity.UpgradeHealth(weapon.bullet.power.currentValue,true);
    }

    
}
