
using System;
[Serializable]
public class Entity
{
    protected EntityUnitType entityUnitType;
    protected WeaponType WeaponType;
    protected BaseAttribute baseAttribute;
    protected Weapon weapon;
    protected BaseStatus baseStatus;
    protected StatusEffect statusEffect;
    protected EntityView View;
    public bool Ally;
    protected int BaseHPInit = 0;

    public virtual void SetEntityView(EntityView _entityView) {
        View = _entityView;
    }
    public virtual void SetAlly() { }
    public virtual void Attack() { }
    public virtual void Dead() {
        baseStatus.SetLive(false);
        View.Dead();
    }
    //public virtual void FindTarget() { }
    public virtual void TakeDamage(float _damage) {
        GetCurrentAttribute().health.currentValue -= _damage;
        View.TakeDamage(_damage);
        if (GetCurrentAttribute().health.currentValue <= 0) {
            Dead();
            //View.Dead();
        }
    }
    public virtual void InitBaseStatus() {
        baseStatus = new BaseStatus();
    }
    public virtual Weapon GetWeaponData() {
        return weapon;
    }
    public virtual BaseStatus GetBaseStatus() {
        return baseStatus;
    }
    public virtual void SetEntityData(BaseAttribute _baseattribute, EntityUnitType _entityUnitType, WeaponType weaponType) {
        entityUnitType = _entityUnitType;
        baseAttribute = _baseattribute;
        WeaponType = weaponType;
        weapon = new Weapon(weaponType, GetCurrentAttribute().atkspeed, GetCurrentAttribute().power, GetCurrentAttribute().critrate, GetCurrentAttribute().critdamage, GetCurrentAttribute().projectileSpeed, GetCurrentAttribute().areaDamageRadius);
    }

    public virtual void ReplaceAttribute(BaseAttribute _baseattribute) {
        baseAttribute = new BaseAttribute(_baseattribute.power.currentValue, _baseattribute.health.currentValue, _baseattribute.atkspeed.currentValue, _baseattribute.critrate.currentValue,
            _baseattribute.critdamage.currentValue, _baseattribute.armor.currentValue, _baseattribute.defense.currentValue, _baseattribute.moveSpeed.currentValue, _baseattribute.rotateSpeed.currentValue,
            _baseattribute.radius.currentValue, _baseattribute.attackDistance.currentValue, _baseattribute.projectileSpeed.currentValue, _baseattribute.areaDamageRadius.currentValue);
        weapon = new Weapon(WeaponType, GetCurrentAttribute().atkspeed, GetCurrentAttribute().power, GetCurrentAttribute().critrate, GetCurrentAttribute().critdamage, GetCurrentAttribute().projectileSpeed, GetCurrentAttribute().areaDamageRadius);
        BaseHPInit = (int)baseAttribute.health.currentValue;
        if (PopupControl.Instance != null)
        {
            PopupControl.Instance.SetBaseValueUIUpgrade(entityUnitType, baseAttribute.power.currentValue, baseAttribute.health.currentValue, baseAttribute.atkspeed.currentValue);
        }
    }
    public virtual void ApplyAttributeUpgradeWhenPlay(BaseAttribute _plus) {
        baseAttribute.UpgradeAllAttribute(_plus);
        UpdateHealthView();

        weapon = new Weapon(WeaponType, GetCurrentAttribute().atkspeed, GetCurrentAttribute().power, GetCurrentAttribute().critrate, GetCurrentAttribute().critdamage, GetCurrentAttribute().projectileSpeed, GetCurrentAttribute().areaDamageRadius);
    }
    public virtual BaseAttribute GetCurrentAttribute() {
        return baseAttribute;
    }
    public virtual void UpgradeHealth(float _plusHealthValue,bool _visualHeal = false) {
        GetCurrentAttribute().UpgradeAttributeByType(GetCurrentAttribute().health, _plusHealthValue);
        UpdateHealthView(_visualHeal);
    }
    public virtual void UpdateHealthView(bool _visualHeal = false) {
        View.UpdateHealthView(_visualHeal);
    }
    public virtual void UpgradeAtkSpd(float _plusAtkSpdValue) {
        GetCurrentAttribute().UpgradeAttributeByType(GetCurrentAttribute().atkspeed, _plusAtkSpdValue);
        weapon = new Weapon(WeaponType, GetCurrentAttribute().atkspeed, GetCurrentAttribute().power, GetCurrentAttribute().critrate, GetCurrentAttribute().critdamage, GetCurrentAttribute().projectileSpeed, GetCurrentAttribute().areaDamageRadius);
    }
    public virtual void UpgradePower(float _plusPowerValue) {
        GetCurrentAttribute().UpgradeAttributeByType(GetCurrentAttribute().power, _plusPowerValue);
        weapon = new Weapon(WeaponType, GetCurrentAttribute().atkspeed, GetCurrentAttribute().power, GetCurrentAttribute().critrate, GetCurrentAttribute().critdamage, GetCurrentAttribute().projectileSpeed, GetCurrentAttribute().areaDamageRadius);
    }
   
}
