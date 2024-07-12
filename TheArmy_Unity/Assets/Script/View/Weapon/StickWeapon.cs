using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickWeapon : WeaponView
{
    [SerializeField] StickBullet StickBullet;
    [SerializeField] GameObject BulletPreview;
    private StickBullet cloneBullet;

    float timetween1 = 0.4f, timetween2 = 0.2f, timetween3 = 0.2f,timetween4 = 0.5f;

    public override void CaculatorLimitFireRate()
    {
        base.CaculatorLimitFireRate();
        limitFireRate = timetween1 + timetween2 + timetween3 + 0.2f; // 0.2 =  time RotateBase
    }
    private void Awake()
    {
        transform.localPosition = new Vector3(0f, 0, 0);
    }

    public override void Attack()
    {
        //Debug.LogError($"{transform.parent.name} : ATTACK BOW");

        //transform.DOLocalMoveY(-0.1f, 0.4f).SetEase(Ease.Unset).OnComplete(() => {
        transform.DOLocalMoveY(-0.06f, timetween1).SetEase(Ease.Linear).OnComplete(() => {
            transform.DOLocalMoveY(0.06f, timetween2);
            BulletPreview.transform.DOLocalMoveY(0.26f, timetween3).OnComplete(() => {
                cloneBullet = Instantiate(StickBullet, this.transform);
                cloneBullet.transform.localPosition = new Vector2(BulletPreview.transform.localPosition.x, BulletPreview.transform.localPosition.y);
                cloneBullet.SetData(entity, weapon.bullet, directionTarget, target);

                cloneBullet.gameObject.SetActive(true);
                Fire();
                transform.DOLocalMoveY(0f, timetween4).SetEase(Ease.Unset).SetDelay(timetween1).SetDelay(0.2f);
                BulletPreview.transform.DOLocalMoveY(-0.323f,0.1f).SetDelay(0.1f);;
                //BulletPreview.transform.localPosition = new Vector2(0, -0.323f);
                //DOVirtual.DelayedCall(0.05f, () => {
                    
                //});
               
            });
           
        });


    }
    public override void Fire()
    {
        cloneBullet.transform.parent = null;
        cloneBullet.SetMove(true);
    }
}
