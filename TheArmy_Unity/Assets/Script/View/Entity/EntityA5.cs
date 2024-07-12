using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityA5 : EntityView
{
    public override void FindTargetByDistance() {
        NextTarget = GameController.Instance.GetAllyLowHP(this,entity.Ally);
        if (NextTarget != null)
        {
            MoveToMonoTarget();
            weaponView.SetSpecialEntityTarget((EntityView)NextTarget);
        }
        
    }
}
