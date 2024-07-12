using System;
using UnityEngine;
[Serializable]
public class Bullet
{
    public BulletType bulletType;
    public Power power;
    public Critrate critate;
    public Critdamage critdamage;
    public ProjectileSpeed projectileSpeed;
    public AreaDamageRadius areaDamageRadius;

    public Bullet(BulletType bulletType, Power power, Critrate critrate, Critdamage critdamage,ProjectileSpeed projectileSpeed, AreaDamageRadius areaDamageRadius) {
        this.bulletType = bulletType;
        this.power = power;
        this.critate = critrate;
        this.critdamage = critdamage;
        this.projectileSpeed = projectileSpeed;
        this.areaDamageRadius = areaDamageRadius;
    }

    public virtual void DealDamageToEntity(Entity _entityTarget) {
        _entityTarget.TakeDamage(power.currentValue);
    }

    public virtual void DealDamageToMono(MonoBehaviour _mono) {
        Brick _brick = (Brick)_mono;
        _brick.TakeDamage(power.currentValue);
    }
}

public enum BulletType {
    SPEAR = 0,
    BOW = 1,
    GUN = 2,
    SHURIKEN = 3,
}
