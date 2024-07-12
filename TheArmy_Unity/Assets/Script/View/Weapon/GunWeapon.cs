using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : WeaponView
{
    private void Awake()
    {
        transform.localPosition = new Vector3(0.45f, 0, 0);
    }

}
