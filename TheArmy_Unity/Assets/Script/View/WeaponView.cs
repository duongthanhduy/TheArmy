using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    public Entity entity;
    public Weapon weapon;
    public  WeaponType weaponType;
    public Vector2 directionTarget;
    public Transform target;
    public EntityView SpecialTarget;

    public float limitFireRate { get; protected set; } = 0.3f;
    public virtual void SetWeapon(Entity _entity, Weapon _weapon) {
        weapon = _weapon;
        entity = _entity;
    }
    public virtual void UpdateDirection(Vector2 _directionTarget) {
        directionTarget = _directionTarget;
    }
    public virtual void SetTarget(Transform _target) {
        target = _target;
    }
    public virtual void SetSpecialEntityTarget(EntityView _target) {
        SpecialTarget = _target;
    }

    public virtual void SetWeaponView() {
        
    }
    public virtual void CaculatorLimitFireRate() { } 

    public virtual void Attack() { }

    public virtual void Fire() { }
}
