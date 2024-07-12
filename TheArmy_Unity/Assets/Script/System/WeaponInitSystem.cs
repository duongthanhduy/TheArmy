using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponInitSystem : MonoBehaviour
{
    public static WeaponInitSystem Instance;
    [SerializeField] List<WeaponView> weaponViews;
    private void Awake()
    {
        Instance = this;
    }

    public WeaponView InitWeapon(WeaponType _weaponType, Transform Parent) {
       
        return Instantiate(weaponViews.Find(x => x.weaponType == _weaponType), Parent); 
    }
}
