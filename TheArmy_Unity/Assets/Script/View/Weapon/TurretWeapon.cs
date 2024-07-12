using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TurretWeapon : WeaponView
{
    [SerializeField] TurretBullet TurretBullet;
    private TurretBullet cloneBullet;
    Tweener tweener,tweener2,tweener3 = null;
    float timetween1 = 0.2f, timetween2 = 0.1f,timeween3 = 0.1f,timeween4 = 0.8f;

    public override void CaculatorLimitFireRate()
    {
        base.CaculatorLimitFireRate();
        limitFireRate = timetween1 + timetween2 + timeween3 * 3 + timeween4;
    }
    private void Awake()
    {
        transform.localPosition = new Vector3(0f, 0f, 0);
    }

    public override void SetWeapon(Entity _entity, Weapon _weapon)
    {
        base.SetWeapon(_entity, _weapon);
        //direction = _direction;
    }

    public override void SetWeaponView()
    {

    }

    public override void Attack()
    {
        if (tweener != null) {
            tweener.Kill();
        }
        if (tweener2 != null) {
            tweener2.Kill();
        }
        if (tweener3 != null) {
            tweener3.Kill();    
        }
        Vector2 direction = (target.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        tweener = transform.DORotateQuaternion(targetRotation, timetween1).SetEase(Ease.Linear).OnComplete(() => {
            cloneBullet = Instantiate(TurretBullet, this.transform);
            cloneBullet.SetData(entity, weapon.bullet, directionTarget, target);
            cloneBullet.transform.localPosition = new Vector2(0, 0.019f);
            //cloneBow.transform.localRotation =  Quaternion.Euler(0,0,0);
            cloneBullet.gameObject.SetActive(true);
            Fire();
            tweener2 = transform.DOScaleY(0.6f, timetween2).SetDelay(timeween3).OnComplete(() => {
               tweener3 = transform.DOScaleY(1, timeween4).SetDelay(timeween3);
            });
        });
        

        //Invoke(nameof(Fire),0.2f);

    }
    public override void Fire()
    {
        cloneBullet.transform.parent = null;
        cloneBullet.SetMove(true);
    }
}
