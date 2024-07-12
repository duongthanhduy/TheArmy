using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ally : Entity
{
   
    public override void SetAlly()
    {
        Ally = true;
        EventDispatcher.Instance.AddListener(EventName.UPGRADE_HEALTH, UpgradeHealth);
        EventDispatcher.Instance.AddListener(EventName.UPGRADE_POWER, UpgradePower);
        EventDispatcher.Instance.AddListener(EventName.UPGRADE_ATKSPD, UpgradeAtkSpd);
    }
   
    public void UpgradeHealth(EventName evn, object ob) {
        Tuple<EntityUnitType,float> entityType_PlusValue = (Tuple<EntityUnitType, float>)ob;
        if (entityType_PlusValue.Item1 != entityUnitType) {
            return;
        }
        float _plusValue = entityType_PlusValue.Item2;
        UpgradeHealth(_plusValue);
    }
    public void UpgradeAtkSpd(EventName evn, object ob)
    {
        Tuple<EntityUnitType, float> entityType_PlusValue = (Tuple<EntityUnitType, float>)ob;
        if (entityType_PlusValue.Item1 != entityUnitType)
        {
            return;
        }
        float _plusValue = entityType_PlusValue.Item2;
        UpgradeAtkSpd(_plusValue);
    }

    public void UpgradePower(EventName evn, object ob)
    {
        Tuple<EntityUnitType, float> entityType_PlusValue = (Tuple<EntityUnitType, float>)ob;
        if (entityType_PlusValue.Item1 != entityUnitType)
        {
            return;
        }
        float _plusValue = entityType_PlusValue.Item2;
        UpgradePower(_plusValue);
    }

    public override void Dead()
    {
        base.Dead();
        EventDispatcher.Instance.RemoveListener(EventName.UPGRADE_HEALTH, UpgradeHealth);
        EventDispatcher.Instance.RemoveListener(EventName.UPGRADE_POWER, UpgradePower);
        EventDispatcher.Instance.RemoveListener(EventName.UPGRADE_ATKSPD, UpgradeAtkSpd);
        GameController.Instance.AllyDead();
    }
}
