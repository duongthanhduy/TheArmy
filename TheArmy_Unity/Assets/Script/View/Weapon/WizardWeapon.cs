using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardWeapon : WeaponView
{
    [SerializeField] WizardBullet wizardBullet;
    private WizardBullet cloneBullet;
    float timetween1 = 0.2f, timetween2 = 0.2f, timetween3 = 0.4f;

    public override void CaculatorLimitFireRate()
    {
        base.CaculatorLimitFireRate();
       limitFireRate = timetween1 + timetween2 + timetween3 + 0.2f; // 0.2 =  time RotateBase
    }
    private void Awake()
    {
        transform.localPosition = new Vector3(0.25f, 0f, 0);
    }

    public override void Attack()
    {
        //Debug.LogError($"{transform.parent.name} : ATTACK BOW");

        //transform.DOLocalMoveY(-0.1f, 0.4f).SetEase(Ease.Unset).OnComplete(() => {
            transform.DOLocalMoveY(0.2f, timetween2).SetEase(Ease.Unset).OnComplete(() => {
                cloneBullet = Instantiate(wizardBullet, this.transform);
                cloneBullet.SetData(entity, weapon.bullet, directionTarget, target);
                cloneBullet.transform.localPosition = new Vector2(0, 0.1f);
                //cloneBow.transform.localRotation =  Quaternion.Euler(0,0,0);
                cloneBullet.gameObject.SetActive(true);
                Fire();
                transform.DOLocalMoveY(0f, timetween3).SetEase(Ease.Unset).SetDelay(timetween1);
                transform.DOLocalRotate(new Vector3(0, 0, 0), timetween3).SetEase(Ease.Unset).SetDelay(timetween1);
            });

            transform.DOLocalRotate(new Vector3(0,0,-15), timetween2);
        //});


        
        //Invoke(nameof(Fire),0.2f);

    }
    public override void Fire()
    {
        cloneBullet.transform.parent = null;
        cloneBullet.SetMove(true);
    }
}
