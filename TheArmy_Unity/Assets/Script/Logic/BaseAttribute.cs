using System;
[Serializable]
public class BaseAttribute
{
    public Power power;
    public Health health;
    public Atkspeed atkspeed;
    public Critrate critrate;
    public Critdamage critdamage;

    public Armor armor;
    public Defense defense;
    public MoveSpeed moveSpeed;
    public RotateSpeed rotateSpeed;
    public Radius radius; // size avatar collider
    public AttackDistance attackDistance;
    public ProjectileSpeed projectileSpeed;
    public AreaDamageRadius areaDamageRadius;


    public void UpgradeAttributeByType(Attribute _attribute,float _plusValue) {
        _attribute.Upgrade(_plusValue);
    }
    public void UpgradeAllAttribute(BaseAttribute _Plus) {
        power.Upgrade(_Plus.power.currentValue);
        health.Upgrade(_Plus.health.currentValue);
        atkspeed.Upgrade(_Plus.atkspeed.currentValue);
        critrate.Upgrade(_Plus.critrate.currentValue);
        critdamage.Upgrade(_Plus.critdamage.currentValue);
        armor.Upgrade(_Plus.armor.currentValue);
        defense.Upgrade (_Plus.defense.currentValue);
        moveSpeed.Upgrade(_Plus.moveSpeed.currentValue);
        rotateSpeed.Upgrade(_Plus.rotateSpeed.currentValue);
        radius.Upgrade(_Plus.radius.currentValue);
        attackDistance.Upgrade(_Plus.attackDistance.currentValue);
        projectileSpeed.Upgrade(_Plus.projectileSpeed.currentValue);
        areaDamageRadius.Upgrade(_Plus.areaDamageRadius.currentValue);
    }

    public BaseAttribute(int _powerlevel = 1, int _healthlevel = 1,int _atkSpeedlevel = 1,int _scritRatelevel = 1,int _critDamageLevel = 1, int _armorLevel = 1, int _DefLeel = 1,int _moveSpeedlevel = 1, int _rotateSpeed = 1,int _radius = 1, int _attackDistance = 1,int _projectileSpeed = 1,int _areaDamageRadius = 1) {
        power = new Power(_powerlevel);
        health = new Health(_healthlevel);
        atkspeed = new Atkspeed(_atkSpeedlevel);
        critrate = new Critrate(_scritRatelevel);
        critdamage = new Critdamage(_critDamageLevel);
        armor = new Armor(_armorLevel);
        defense = new Defense(_DefLeel);
        moveSpeed = new MoveSpeed(_moveSpeedlevel);
        rotateSpeed = new RotateSpeed(_rotateSpeed);
        radius = new Radius(_radius);
        attackDistance = new AttackDistance(_attackDistance);
        projectileSpeed = new ProjectileSpeed(_projectileSpeed);
        areaDamageRadius = new AreaDamageRadius(_areaDamageRadius);

    }
    public BaseAttribute(float _power =0, float _health = 0, float _atkSpeed = 0, float _scritRate = 0, float _critDamage = 0, float _armor = 0, float _Def = 0, float _moveSpeed = 0, float _rotate = 0, float _radius = 0, float _attackDistance = 0, float _projectileSpeed =0, float _areaDamageRadius = 0) {
        power = new Power(_power);
        health = new Health(_health);
        atkspeed = new Atkspeed(_atkSpeed);
        critrate = new Critrate(_scritRate);
        critdamage = new Critdamage(_critDamage);
        armor = new Armor(_armor);
        defense = new Defense(_Def);
        moveSpeed = new MoveSpeed(_moveSpeed);
        rotateSpeed = new RotateSpeed(_rotate);
        radius = new Radius(_radius);
        attackDistance = new AttackDistance(_attackDistance);
        projectileSpeed = new ProjectileSpeed(_projectileSpeed);
        areaDamageRadius = new AreaDamageRadius(_areaDamageRadius);
    }
}

[Serializable]
public class Power : Attribute
{
    public int level { get;set;} = 1;
    public int price => 1 + (level * 2);

    //public float currentValue;
    public float valueBonus = 0;
    public Power(int _level) {
        level = _level;
        currentValue+= valueBonus;
    }
    public Power(float _power) {
        currentValue = _power;
    }
}

[Serializable]
public class Health : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue;
    public float valueBonus = 0;
    public Health(int _level)
    {
        level = _level;
        currentValue += valueBonus;
    }
    public Health(float _health) {
        currentValue = _health;
    }
}

[Serializable]
public class Atkspeed : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue;
    public float valueBonus = 0;
    public Atkspeed(int _level)
    {
        level = _level;
        currentValue+=valueBonus;
    }
    public Atkspeed(float _atkspeed) {
        currentValue = _atkspeed;
    }
}

[Serializable]
public class Critrate : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue;
    public float valueBonus = 0;
    public Critrate(int _level)
    {
        level = _level;
        currentValue+=valueBonus;
    }
    public Critrate(float _critrate) {
        currentValue = _critrate;
    }
}

[Serializable]
public class Critdamage : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue;
    public float valueBonus = 0;

    public Critdamage(int _level)
    {
        level = _level;
        currentValue+=valueBonus;
    }
    public Critdamage(float _critdamage)
    {
        currentValue = _critdamage;
    }
}

[Serializable]
public class Defense : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue;
    public float valueBonus = 0;
    public Defense(int _level)
    {
        level = _level;
        currentValue+=valueBonus;
    }
    public Defense(float _defense) {
        currentValue =  _defense;
    }
}

[Serializable]
public class Armor : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue; 
    public float valueBonus = 0;
    public Armor(int _level)
    {
        level = _level;
        currentValue+=valueBonus;
    }
    public Armor(float _armor) {
        currentValue = _armor;
    }
}

[Serializable]
public class MoveSpeed : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue;
    public float valueBonus = 0;
    public MoveSpeed(int _level)
    {
        level = _level;
        currentValue += valueBonus;
    }
    public MoveSpeed(float _moveSpeed) {
        currentValue = _moveSpeed;
    }
}
[Serializable]
public class RotateSpeed : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue; 
    public float valueBonus = 0;
    public RotateSpeed(int _level)
    {
        level = _level;
        currentValue += valueBonus;
    }
    public RotateSpeed(float _rotateSpeed) {
        currentValue = _rotateSpeed;    
    }
}
[Serializable]
public class Radius : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue ;
    public float valueBonus = 0;
    public Radius(int _level)
    {
        level = _level;
        currentValue += valueBonus;
    }
    public Radius(float _radius) {
        currentValue = _radius;
    }
}
[Serializable]
public class AttackDistance : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue; 
    public float valueBonus = 0;
    public AttackDistance(int _level)
    {
        level = _level;
        currentValue += valueBonus;
    }
    public AttackDistance(float _attackDistance) {
        currentValue = _attackDistance;
    }
}
[Serializable]
public class ProjectileSpeed : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue; 
    public float valueBonus = 0;
    public ProjectileSpeed(int _level)
    {
        level = _level;
        currentValue += valueBonus;
    }
    public ProjectileSpeed(float _projectileSpeed) {
        currentValue = _projectileSpeed;
    }
}
[Serializable]
public class AreaDamageRadius : Attribute
{
    public int level { get; set; } = 1;
    public int price => 1 + (level * 2);

    //public float currentValue; 
    public float valueBonus = 0;
    public AreaDamageRadius(int _level)
    {
        level = _level;
        currentValue += valueBonus;
    }
    public AreaDamageRadius(float _areaDamageRadius) {
        currentValue = _areaDamageRadius;
    }
}

[Serializable]
public class Attribute {
    public float currentValue = 0;
    
    public virtual void Upgrade(float _plusValue) {
        currentValue += _plusValue;
    }
}

public enum AttributeType {
    power = 0,
    health =1,
    atkspeed = 2,
    critrate =3,
    critdamage = 4,
    armor = 5,
    defense = 6,
    moveSpeed = 7,
    rotateSpeed = 8,
    radius = 9,
    attackDistance = 10,
    projectileSpeed = 11,
    areaDamageRadius = 12
}


