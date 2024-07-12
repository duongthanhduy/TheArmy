using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpearWeapon : WeaponView
{
    [SerializeField] SpearBullet spearBullet;
    float timetween1 = 0.4f, timetween2 = 0.1f, timetween3 = 0.5f;
    public override void SetWeapon(Entity _entity,Weapon _weapon)
    {
        base.SetWeapon(_entity, _weapon);
        spearBullet.SetData(_entity, _weapon.bullet);
        CaculatorLimitFireRate();
    }
    private void Awake()
    {
        transform.localPosition = new Vector3(0.25f,0,0);
    }
    public override void SetWeaponView()
    {

    }

    public override void Attack() {
        transform.DOLocalMoveY(-0.1f, timetween1).SetEase(Ease.Unset).OnComplete(() => {
            transform.DOLocalMoveY(0.2f, timetween2).SetEase(Ease.Unset).OnComplete(() => {
                Fire();
                transform.DOLocalMoveY(0f, timetween3).SetEase(Ease.Unset).SetDelay(0.2f);
            });
        });
        
    }
    public override void CaculatorLimitFireRate()
    {
        base.CaculatorLimitFireRate();
        limitFireRate = timetween1 + timetween2 + timetween3 ;
    }
    public override void Fire()
    {
        spearBullet.gameObject.SetActive(true);
        Invoke(nameof(OffSpearBullet),0.2f);
    }

    private void OffSpearBullet() {
        spearBullet.gameObject.SetActive(false);
        spearBullet.ResetDealDamage();
    }

}
