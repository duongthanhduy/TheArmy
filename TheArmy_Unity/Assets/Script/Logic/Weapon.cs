
public class Weapon
{
    public WeaponType weaponType;
    public Atkspeed atkspeed;
    public Bullet bullet;

    public Weapon(WeaponType weaponType, Atkspeed atkspeed, Power power, Critrate critrate, Critdamage critdamage, ProjectileSpeed projectileSpeed,AreaDamageRadius areaDamageRadius) {
        this.weaponType = weaponType;
        this.atkspeed = atkspeed;
        bullet = new Bullet((BulletType)(weaponType), power, critrate, critdamage, projectileSpeed, areaDamageRadius);
    }
   
    public virtual void CaculateFireRate() { }
    public virtual void Attack() { }
   
}

public enum WeaponType {
    SPEAR = 0,
    BOW = 1,
    HEALTH = 2,
    WIZARD = 3,
    BLADE = 4,
    TURRET = 5,
    BIGBOW = 6,
    STICK = 7,
}


