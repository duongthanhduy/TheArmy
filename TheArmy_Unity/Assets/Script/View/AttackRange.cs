using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private EntityView EntitySrouce;
    private EntityView EntityTarget;
    private MonoBehaviour MonoTarget;
    private float attackrange;
    public void SetEntity(EntityView _entity) {
        EntitySrouce = _entity;
    }
    public void UpdateRange(float _attackRange) {
        //Debug.LogError($"range: {_attackRange}");
        //circleCollider.radius = _attackRange + GameVariableConfig.AttackRange_Bonus;
        attackrange = _attackRange + GameVariableConfig.AttackRange_Bonus;;
    }
    public bool CheckInRange(float _distance) {
        return _distance <= attackrange;
    }

    public void RemoveEntityTarget() {
        EntityTarget = null;
    }

    public void RemoveMonoTarget() {
        MonoTarget = null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }
#endif
}
