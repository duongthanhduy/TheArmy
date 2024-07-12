using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitIconSystem : MonoBehaviour
{

    //public enum WeaponType
    //{
    //    SPEAR = 0,
    //    BOW = 1,
    //    GUN = 2,
    //    SHURIKEN = 3,
    //    HEALTH = 4,
    //}

    //public enum EntityUnitType
    //{
    //    A1, A2, A3, A4, A5, A6, A7,
    //    E1, E2, E3, E4, E5, E6, E7,
    //    B1, B2, B3, B4, B5, B6, B7,
    //}

    public static InitIconSystem Instance;
    [SerializeField] Sprite[] IconBodys;
    [SerializeField] Sprite[] IconWeapons;

    private void Awake()
    {
        
        Instance = this;
    }
    
    public Sprite GetIconBody(EntityUnitType _type) {
        return IconBodys[(int)_type];
    }

    public Sprite GetIconWeapon(WeaponType _type) {
        return IconWeapons[(int)_type];
    }
}
