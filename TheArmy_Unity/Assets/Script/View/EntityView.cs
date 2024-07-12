using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

[Serializable]
public class EntityView : MonoBehaviour
{
    public EntityUnitType EntityType;
    public GameObject Model;
    [HideInInspector] public Entity entity;
    [HideInInspector] public WeaponView weaponView;
    public UnitData unitData;
    [HideInInspector] public EntityView EntityTarget;
    [HideInInspector] public MonoBehaviour MonoTarget;
    [HideInInspector] protected Rigidbody2D rigidbody2D;
    public AttackRange attackRange;
    [HideInInspector] public MonoBehaviour NextTarget;

    protected float TimeLastAttack;
    public TMP_Text txtHealth;
    [HideInInspector] public bool InrangeAttack = false;
    private List<EntityUnitType> UnitRange = new List<EntityUnitType>{ EntityUnitType.A2, EntityUnitType.A5, EntityUnitType.A4,EntityUnitType.E5 };
    //public Animator animator;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public virtual void SetData(Entity _entity) { 
        entity = _entity;
        entity.InitBaseStatus();
        ReplaceData(unitData);
        InitWeapon();
        entity.SetAlly();
        attackRange.SetEntity(this);
        attackRange.UpdateRange(entity.GetCurrentAttribute().attackDistance.currentValue);
        entity.GetBaseStatus().SetLive(true);
        entity.SetEntityView(this);
        txtHealth.text = $"{Math.Ceiling(entity.GetCurrentAttribute().health.currentValue)}";
    }
   
    public virtual void Attack() {
        TimeLastAttack = Time.time;
        //Debug.LogError($"{EntityType} -ATTACK");
        RotateToTargetAttack(() => {
            weaponView.Attack();
        });
       
        //Target.TakeDamage(entity.get);
    }
    public virtual void Dead() {
        gameObject.SetActive(false);
        GameController.Instance.RemoveMono(this);
        GameController.Instance.RemoveEntity(this);
        Destroy(gameObject);
       // entity.GetBaseStatus().SetLive(false);
    }
    public virtual void FindTargetByDistance() {
       // EntityTarget = GameController.Instance.GetEntityNearest(this);
        NextTarget = GameController.Instance.GetTargetNearest(this,entity.Ally);
        if (NextTarget != null) {
            MoveToMonoTarget();
        }
        
    }
    
    public virtual void MoveToMonoTarget()
    {
        //range
        if(UnitRange.Contains(EntityType))
        {
            if (Attacking || InrangeAttack)
            {
                rigidbody2D.velocity = Vector2.zero;
                return;
            }
        }
        //movingToEntity = false;
        // movingToMono = true;
        Vector2 direction = (NextTarget.transform.position - transform.position);

        Vector2 normalizedDirection = direction.normalized;
        //Debug.LogError($"DICRECTION: {direction}");
        Vector2 movement = normalizedDirection * entity.GetCurrentAttribute().moveSpeed.currentValue * Time.deltaTime * GameVariableConfig.MoveSpeed_Bonus;
        //rigidbody2D.AddForce(movement);
        //if (rigidbody2D.velocity.magnitude > entity.GetCurrentAttribute().moveSpeed.currentValue * GameVariableConfig.MoveSpeed_Bonus) {
        //    rigidbody2D.velocity = movement;
        //}
        //rigidbody2D.velocity = movement;
        Vector2 smoothVelocity = rigidbody2D.velocity;
        Vector2 targetVelocity = Vector2.SmoothDamp(rigidbody2D.velocity, movement, ref smoothVelocity, 6f * Time.deltaTime);
        rigidbody2D.velocity = targetVelocity;
        //rigidbody2D.AddForce(movement);
        //RotateWhenMovetoMono();
        RotateWhenMovetoMono();
        InrangeAttack = attackRange.CheckInRange(Vector3.Distance(transform.position, NextTarget.transform.position));
        if (InrangeAttack)
        {
            if (CanAttackEntity(NextTarget) || CanAttackTargetBrick(NextTarget))
            {
                Attack();
            }

        }
        //else if(!Attacking) {
        //    RotateWhenMovetoMono();
        //}
    }

    protected bool Attacking = false;
    //protected bool movingToEntity = false;
   // protected bool movingToMono = false;
    //public virtual void RotateWhenMovetoEntity() {
    //    Vector2 direction = (EntityTarget.transform.position - transform.position).normalized;
    //    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, entity.GetCurrentAttribute().rotateSpeed.rotateSpeed * Time.deltaTime * GameVariableConfig.RotateSpeed_Bonus);
    //}
    //public virtual void RotateWhenMovetoMono()
    //{
    //    Vector2 direction = (MonoTarget.transform.position - transform.position).normalized;
    //    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, entity.GetCurrentAttribute().rotateSpeed.rotateSpeed * Time.deltaTime * GameVariableConfig.RotateSpeed_Bonus);
    //}

    public virtual void RotateWhenMovetoMono()
    {
        //if (Attacking || InrangeAttack)
        //{
        //    return;
        //}
        
        Vector2 direction = (NextTarget.transform.position - Model.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        Model.transform.rotation = Quaternion.Slerp(Model.transform.rotation, targetRotation, entity.GetCurrentAttribute().rotateSpeed.currentValue * Time.deltaTime * GameVariableConfig.RotateSpeed_Bonus);
    }
    Tweener tweener = null;
    public virtual void RotateToTargetAttack(Action _completed) {
        Attacking = true;

        if (entity.GetCurrentAttribute().rotateSpeed.currentValue == 0)
        {
            Vector2 _direction = (NextTarget.transform.position - transform.position);

            Vector2 normalizedDirection = _direction.normalized;
            weaponView.UpdateDirection(normalizedDirection);
            weaponView.SetTarget(NextTarget.transform);
            _completed?.Invoke();
            Attacking = false;
            return;
        }

        Transform _target;
        if (tweener != null) {
            tweener.Kill();
        }
        Vector2 direction = (NextTarget.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        tweener = Model.transform.DORotateQuaternion(targetRotation, 0.2f).SetEase(Ease.Linear).OnComplete(() => {
            Vector2 _direction = (NextTarget.transform.position - transform.position);

            Vector2 normalizedDirection = _direction.normalized;


            weaponView.UpdateDirection(normalizedDirection);
            weaponView.SetTarget(NextTarget.transform);
            _completed?.Invoke();
            Attacking = false;
        });
    }
    public virtual void TakeDamage(float _damage) {
        //entity.TakeDamage(_damage);
        txtHealth.text = $"{Math.Ceiling(entity.GetCurrentAttribute().health.currentValue)}";
    }
    public virtual void InitWeaponView() {
        weaponView = WeaponInitSystem.Instance.InitWeapon(entity.GetWeaponData().weaponType,Model.transform);
        weaponView.SetWeapon(entity,entity.GetWeaponData());
    }

    public virtual void InitWeapon() {
        InitWeaponView();
    }
    public virtual void ReplaceData(UnitData _unitData) {
        entity.ReplaceAttribute(new BaseAttribute(_unitData.BasePower, _unitData.BaseHealth, _atkSpeed: _unitData.BaseAttackSpeed, _scritRate:0, _critDamage:0, _armor: 0,
           _Def:0, _moveSpeed: _unitData.MoveSpeed, _rotate: _unitData.RotateSpeed, _radius: _unitData.Radius, _attackDistance:_unitData.AttackDistance,
           _projectileSpeed:_unitData.ProjectileSpeed, _areaDamageRadius:_unitData.AreaDamageRadius));
        //UnityEngine.Debug.LogError($"{EntityType}:---> Power:{entity.GetCurrentAttribute().power.currentValue}, Health: {entity.GetCurrentAttribute().health.currentValue}");
    }
    private void FixedUpdate()
    {
        if (!entity.GetBaseStatus().Live) {
            return;
        }
        //if (Target == null) {
        FindTargetByDistance();
        if (NextTarget != null) {
            InrangeAttack = attackRange.CheckInRange(Vector3.Distance(transform.position, NextTarget.transform.position));
            if (InrangeAttack)
            {
                if (CanAttackEntity(NextTarget) || CanAttackTargetBrick(NextTarget))
                {
                    Attack();
                }

            }
        }
        
       
        //}
        //if (MonoTarget != null) {
        //    MoveToMonoTarget();
        //    return;
        //}
        //if (EntityTarget != null) {
        //    MoveToEntityTarget();
        //}
    }


    public virtual bool CanAttackEntity(MonoBehaviour _mono) {
        bool result = false;
        EntityView entityView = null;
        entityView = _mono.GetComponent<EntityView>();
        if (entityView == null) {
            result = false;
            return result;
        }
        else {
            if (entity.GetCurrentAttribute().atkspeed.currentValue == 0)
            {
                result = false;
                return result;
            }
            else if (_mono == null)
            {
                result = false;
                return result;
            }
            else
            {
                if (!entityView.entity.GetBaseStatus().Live)
                {
                    result = false;
                    EntityTarget = null;
                    attackRange.RemoveEntityTarget();
                }
                else
                {
                    result = Time.time - TimeLastAttack >= CaculateCoolDownAttack();
                }
            }
            return result;
        }
        
        
    }

    public virtual float CaculateCoolDownAttack() {
        float result = 0;
        float attackSpeed = entity.GetCurrentAttribute().atkspeed.currentValue; // 0.7f  <=> 2
        if (attackSpeed != 0) {
            result = GameVariableConfig.AttackFireRate / attackSpeed;
            if (result < weaponView.limitFireRate) {
                result = weaponView.limitFireRate;
            }
        }
        else {
            result = float.MaxValue;
        }
        //UnityEngine.Debug.LogError($"ATK SPD : {result}");
        return result;
    }

    public virtual void ForceSetTarget(MonoBehaviour _mono) {
        MonoTarget = _mono;

    }

    public virtual bool CanAttackTargetBrick(MonoBehaviour _mono) {
        bool result = false;
        Brick _brick = null;
        _brick = _mono.GetComponent<Brick>();
        if (_brick == null) {
            result = false;
        }
        else {
            if (_brick == null) {
                result = false;
            }
            else {
                if (_brick.health > 0) {
                    result = Time.time - TimeLastAttack >= CaculateCoolDownAttack();
                    
                }
                else {
                    result = false;
                    MonoTarget = null;
                    attackRange.RemoveMonoTarget();
                }
            }
            
        }

        return result;
    }
    public virtual void UpdateHealthView(bool visualHeal) {
        txtHealth.text = $"{Math.Ceiling(entity.GetCurrentAttribute().health.currentValue)}";
        if (visualHeal) {
            txtHealth.color = Color.green;
            DOVirtual.DelayedCall(0.3f,()=> {
                txtHealth.color = Color.black;
            });
        }
    }

   
}

public enum EntityUnitType {
    A1, A2, A3,A4,A5,A6,A7,
    E1, E2, E3, E4, E5, E6, E7,
    B1, B2, B3, B4, B5, B6, B7,
}
