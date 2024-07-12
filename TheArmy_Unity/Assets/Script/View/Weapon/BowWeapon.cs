using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeapon : WeaponView
{
    [SerializeField] BowBullet BowBullet;
    private BowBullet cloneBow;
    public override void CaculatorLimitFireRate()
    {
        base.CaculatorLimitFireRate();
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
        cloneBow = Instantiate(BowBullet,this.transform);
        cloneBow.SetData(entity, weapon.bullet, directionTarget,target);
        cloneBow.transform.localPosition = new Vector2(0,0.019f);
        //cloneBow.transform.localRotation =  Quaternion.Euler(0,0,0);
        cloneBow.gameObject.SetActive(true);
        Fire();
        //Invoke(nameof(Fire),0.2f);
       
    }
    public override void Fire()
    {
        cloneBow.transform.parent = null;
        cloneBow.SetMove(true);
    }
}
