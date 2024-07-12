using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeWeapon : WeaponView
{
   
    private float RotateSpeed = 0;
    [SerializeField] BladeBullet[] bladeBullets;
    public override void SetWeapon(Entity _entity, Weapon _weapon)
    {
        base.SetWeapon(_entity, _weapon);
        for (int i = 0; i < bladeBullets.Length;i++) {
            bladeBullets[i].SetData(_entity, _weapon.bullet);
        }
        //spearBullet.SetData(_entity, _weapon.bullet);
        CaculatorLimitFireRate();
    }
    private void Awake()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }
    private void FixedUpdate()
    {
        if (weapon.atkspeed.currentValue > 10) {
            weapon.atkspeed.currentValue = 10;
        }
        float zRotation = transform.rotation.eulerAngles.z;

        // Tính toán độ quay mới dựa trên tốc độ quay và thời gian giữa các frame
        float newRotation = zRotation - weapon.atkspeed.currentValue * Time.fixedDeltaTime * 500;

        // Áp dụng độ quay mới vào đối tượng
        transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
    }
   
}
