using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBowWeapon : WeaponView
{
    [SerializeField] BigBowBullet BigBowBullet;
    private BigBowBullet cloneBullet;
    float timeween1 = 1f,timeween2 = 0.2f,timeween3 = 0.2f;


    public override void CaculatorLimitFireRate()
    {
        base.CaculatorLimitFireRate();
        limitFireRate = timeween1 + timeween2 + timeween3 + 0.2f; // 0.2f :time rotatebase
    }
    private void Awake()
    {
        transform.localPosition = new Vector3(0f, 0.3f, 0);
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
        //Debug.LogError($"{transform.parent.name} : ATTACK BOW");
        cloneBullet = Instantiate(BigBowBullet, this.transform);
        cloneBullet.SetData(entity, weapon.bullet, directionTarget, target);
        cloneBullet.transform.localPosition = new Vector2(0, 0.019f);
        //cloneBow.transform.localRotation =  Quaternion.Euler(0,0,0);
        cloneBullet.gameObject.SetActive(true);
        cloneBullet.transform.DOLocalMoveY(-0.5f, timeween1).SetDelay(timeween2).OnComplete(() => {
            DOVirtual.DelayedCall(timeween3, () => {
                Fire();
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
