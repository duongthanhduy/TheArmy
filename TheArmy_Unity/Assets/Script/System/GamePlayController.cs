using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    // Start is called before the first frame update
    //public static LevelController Instance;
    public List<MonoBehaviour> AllyViews = new List<MonoBehaviour>();
    public List<EntityView> AllEntityView = new List<EntityView>();
    //public List<EntityView> AllAlly = new List<EntityView>();
    public List<EntityView> AllEnemy = new List<EntityView>();
    public Transform PosSpawAlly;
    private Transform PosSpawEnemy1, PosSpawEnemy2, PosSpawEnemy3, PosSpawEnemy4;
    public List<MonoBehaviour> BrickAndEnemy = new List<MonoBehaviour>();
    public int CurrentCountAlly { get;private set;} = 0;

    public Dictionary<EntityUnitType,BaseAttribute> UpgradedAttribute = new Dictionary<EntityUnitType, BaseAttribute>();

    public void RefrestDataUpgradeWhenPlay(EntityUnitType _entityType ,AttributeType _attributeType, float _newValue, Attribute _attribute = null) {
        if (UpgradedAttribute.ContainsKey(_entityType)) {
            switch (_attributeType) {
                case AttributeType.power:
                    _attribute = UpgradedAttribute[_entityType].power;
                    break;
                    case AttributeType.health:
                    _attribute = UpgradedAttribute[_entityType].health;
                    break;
                    case AttributeType.atkspeed:
                    _attribute = UpgradedAttribute[_entityType].atkspeed;
                    break;
                    case AttributeType.critrate:
                    _attribute = UpgradedAttribute[_entityType].critrate;
                    break;
                    case AttributeType.critdamage:
                    _attribute = UpgradedAttribute[_entityType].critdamage;
                    break;
                    case AttributeType.armor:
                    _attribute = UpgradedAttribute[_entityType].armor;
                    break;
                    case AttributeType.defense:
                    _attribute = UpgradedAttribute[_entityType].defense;
                    break;
                    case AttributeType.moveSpeed:
                    _attribute = UpgradedAttribute[_entityType].moveSpeed;
                    break;
                    case AttributeType.rotateSpeed:
                    _attribute = UpgradedAttribute[_entityType].rotateSpeed;
                    break;
                    case AttributeType.radius:
                    _attribute = UpgradedAttribute[_entityType].radius;
                    break;
                    case AttributeType.attackDistance:
                    _attribute = UpgradedAttribute[_entityType].attackDistance;
                    break;
                    case AttributeType.projectileSpeed:
                    _attribute = UpgradedAttribute[_entityType].projectileSpeed;
                    break;
                    case AttributeType.areaDamageRadius:
                    _attribute = UpgradedAttribute[_entityType].areaDamageRadius;
                    break;
            }
            BaseAttribute baseAttribute = UpgradedAttribute[_entityType];
            baseAttribute.UpgradeAttributeByType(_attribute, _newValue);
            UpgradedAttribute[_entityType] = baseAttribute;
        }
    }
    
    public MonoBehaviour GetTargetNearest(MonoBehaviour _source,bool _ally) {
        MonoBehaviour nearest = null;
        float minDistance = float.MaxValue;
        if (_ally) {
            foreach (MonoBehaviour mono in BrickAndEnemy)
            {
                if (_source == mono) {
                    continue;
                }
                float distance = Vector3.Distance(mono.transform.position, _source.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = mono;
                }
            }
        }
        else {
            foreach (MonoBehaviour mono in AllyViews)
            {
                float distance = Vector3.Distance(mono.transform.position, _source.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = mono;
                }
            }
        }

        return nearest;
    }

    public EntityView GetAllyLowHP(EntityView _source,bool _isAlly) {
        EntityView result = null;
        float minHP = float.MaxValue;
        if (_isAlly) {
            foreach (EntityView mono in AllyViews)
            {
                if (mono == _source || mono.EntityType == EntityUnitType.A5) {
                    continue;
                }
                float hp = mono.entity.GetCurrentAttribute().health.currentValue;

                if (hp < minHP)
                {
                    minHP = hp;
                    result = mono;
                }
            }
        }
        else {
            foreach (EntityView mono in AllEnemy)
            {
                if (mono == _source || mono.EntityType == EntityUnitType.A5)
                {
                    continue;
                }
                float hp = mono.entity.GetCurrentAttribute().health.currentValue;

                if (hp < minHP)
                {
                    minHP = hp;
                    result = mono;
                }
            }
        }
        return result;
    }
    public void RemoveMono(MonoBehaviour _mono) {
        if (BrickAndEnemy.Contains(_mono)) {
            BrickAndEnemy.Remove(_mono);
        }
        if (AllyViews.Contains(_mono)) {
            AllyViews.Remove(_mono);
        }
        
        if (BrickAndEnemy.Count == 0) {
            GameController.Instance.NextLevel();
        }

    }
    public void RemoveEntity(EntityView _entity) {
        if (AllEnemy.Contains(_entity)) {
            AllEnemy.Remove(_entity);
        }
        if (AllyViews.Contains(_entity)) {
            AllyViews.Remove(_entity);
        }
    }
   

    public void SpawnEnemy() {
       // return;
        Enemy E1 = new Enemy();
        E1.SetEntityData(new BaseAttribute(3, 3, 3, 3, 3, 3, 3, 3, 3), EntityUnitType.E1, WeaponType.SPEAR);
        var E1_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.E1, null, PosSpawEnemy1.transform.position, PosSpawEnemy1.transform.rotation);
        E1_View.SetData(E1);

        Enemy E2 = new Enemy();
        E2.SetEntityData(new BaseAttribute(2, 1, 2, 3, 3, 2, 1, 2, 3), EntityUnitType.E2, WeaponType.TURRET);
        var E2_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.E2, null, PosSpawEnemy2.transform.position, PosSpawEnemy2.transform.rotation);
        E2_View.SetData(E2);

        Enemy E3 = new Enemy();
        E3.SetEntityData(new BaseAttribute(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1), EntityUnitType.E6, WeaponType.BIGBOW);
        var E3_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.E6, null, PosSpawEnemy3.transform.position, PosSpawEnemy3.transform.rotation);
        E3_View.SetData(E3);

        Enemy E4 = new Enemy();
        E4.SetEntityData(new BaseAttribute(2, 2, 2, 2, 2, 2, 2, 3, 1), EntityUnitType.E5, WeaponType.STICK);
        var E4_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.E5, null, PosSpawEnemy4.transform.position, PosSpawEnemy4.transform.rotation);
        E4_View.SetData(E4);
        AllEntityView.Add(E1_View);
        AllEntityView.Add(E2_View);
        AllEntityView.Add(E3_View);
        AllEntityView.Add(E4_View);
        BrickAndEnemy.AddRange(AllEntityView);
        AllEnemy.Add(E1_View);
        AllEnemy.Add(E2_View);
        AllEnemy.Add(E3_View);
        AllEnemy.Add(E4_View);
    }

    
    public void SetSpawEnemyPos(Transform pos1, Transform pos2, Transform pos3, Transform pos4) {
        PosSpawEnemy1 = pos1;
        PosSpawEnemy2 = pos2;
        PosSpawEnemy3 = pos3;
        PosSpawEnemy4 = pos4;
    }

    public void AddBrick(List<Brick> _bricks) {
        BrickAndEnemy.AddRange(_bricks);
    }


    private void CreateData() {
        Ally Ally1 = new Ally();
        Ally1.SetEntityData(new BaseAttribute(2,2,2,2,2,2,2,2,2), EntityUnitType.A1 ,WeaponType.SPEAR);
        var Ally1_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.A1, null,PosSpawAlly.transform.position, PosSpawAlly.transform.rotation);
        Ally1_View.SetData(Ally1);

        Ally Ally2 = new Ally();
        Ally2.SetEntityData(new BaseAttribute(1,1,1,1,1,1,1,11,1,1,1,1,1), EntityUnitType.A2, WeaponType.SPEAR);
        var Ally2_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.A2, null, PosSpawAlly.transform.position, PosSpawAlly.transform.rotation);
        Ally2_View.SetData(Ally2);

        Ally Ally3 = new Ally();
        Ally3.SetEntityData(new BaseAttribute(3,3,3,3,3,3,3,3,3), EntityUnitType.A3, WeaponType.BOW);
        var Ally3_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.A3, null, PosSpawAlly.transform.position, PosSpawAlly.transform.rotation);
        Ally3_View.SetData(Ally3);


        //Ally Ally4 = new Ally();
        //Ally4.SetEntityData(new BaseAttribute(1,2,3,2,1,2,3,2,1), EntityUnitType.A4, WeaponType.GUN);
        //var Ally4_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.A4, null, PosSpawAlly.transform.position, PosSpawAlly.transform.rotation);
        //Ally4_View.SetData(Ally4);

        /////////////////////////////////////////////////////

        //Enemy E1 = new Enemy();
        //E1.SetEntityData(new BaseAttribute(3, 3, 3, 3, 3, 3, 3, 3, 3), EntityUnitType.E1, WeaponType.SPEAR);
        //var E1_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.E1, null, PosSpawEnemy1.transform.position, PosSpawEnemy1.transform.rotation);
        //E1_View.SetData(E1);

        //Enemy E2 = new Enemy();
        //E2.SetEntityData(new BaseAttribute(2, 1, 2, 3, 3, 2, 1, 2, 3), EntityUnitType.B4, WeaponType.SPEAR);
        //var E2_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.B4, null, PosSpawEnemy2.transform.position, PosSpawEnemy2.transform.rotation);
        //E2_View.SetData(E2);

        //Enemy E3 = new Enemy();
        //E3.SetEntityData(new BaseAttribute(1,1,1,1,1,1,1,1,1,1,1,1,1), EntityUnitType.E3, WeaponType.BOW);
        //var E3_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.E3, null, PosSpawEnemy3.transform.position, PosSpawEnemy3.transform.rotation);
        //E3_View.SetData(E3);

        //Enemy E4 = new Enemy();
        //E4.SetEntityData(new BaseAttribute(2, 2, 2, 2, 2, 2, 2, 3, 1), EntityUnitType.B1, WeaponType.GUN);
        //var E4_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.B1, null, PosSpawEnemy4.transform.position, PosSpawEnemy4.transform.rotation);
        //E4_View.SetData(E4);

        ///////////////////////////////////////////////////////////

        //entities.Add(Ally1);

        //entities.Add(Ally1);
        //entities.Add(Ally2);
        //entities.Add(Ally3);
        //entities.Add(Ally4);
       
        //entities.Add(E1);
        //entities.Add(E2);
        //entities.Add(E3);
        //entities.Add(E4);

    }

    public void SpawAllyByType(EntityUnitType _type) {
        BaseAttribute _baseAttribute = null;
        if (_type == EntityUnitType.A1) {
            Ally Ally1 = new Ally();
            _baseAttribute = new BaseAttribute(2, 2, 2, 2, 2, 2, 2, 2, 2);
            Ally1.SetEntityData(_baseAttribute, _type, WeaponType.SPEAR);
            var Ally1_View = EntityInitSystem.Instance.InitEntity(_type, null, PosSpawAlly.transform.position, PosSpawAlly.transform.rotation);
            Ally1_View.SetData(Ally1);
            if (UpgradedAttribute.ContainsKey(_type))
            {
                BaseAttribute _PlusValues = UpgradedAttribute[_type];
                Ally1.ApplyAttributeUpgradeWhenPlay(_PlusValues);
            }
            AllyViews.Add(Ally1_View);

        }
        if (_type == EntityUnitType.A2) {
            Ally Ally2 = new Ally();
            _baseAttribute = new BaseAttribute(1, 1, 1, 1, 1, 1, 1, 11, 1, 1, 1, 1, 1);
            
            Ally2.SetEntityData(new BaseAttribute(1, 1, 1, 1, 1, 1, 1, 11, 1, 1, 1, 1, 1), EntityUnitType.A2, WeaponType.BOW);
            var Ally2_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.A2, null, PosSpawAlly.transform.position, PosSpawAlly.transform.rotation);
            Ally2_View.SetData(Ally2);
            if (UpgradedAttribute.ContainsKey(_type))
            {
                BaseAttribute _PlusValues = UpgradedAttribute[_type];
                Ally2.ApplyAttributeUpgradeWhenPlay(_PlusValues);
            }
            AllyViews.Add(Ally2_View);
        }
        if (_type == EntityUnitType.A5)
        {
            Ally Ally5 = new Ally();
            _baseAttribute = new BaseAttribute(1, 1, 1, 1, 1, 1, 1, 11, 1, 1, 1, 1, 1);

            Ally5.SetEntityData(new BaseAttribute(1, 1, 1, 1, 1, 1, 1, 11, 1, 1, 1, 1, 1), EntityUnitType.A5, WeaponType.HEALTH);
            var Ally5_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.A5, null, PosSpawAlly.transform.position, PosSpawAlly.transform.rotation);
            Ally5_View.SetData(Ally5);
            if (UpgradedAttribute.ContainsKey(_type))
            {
                BaseAttribute _PlusValues = UpgradedAttribute[_type];
                Ally5.ApplyAttributeUpgradeWhenPlay(_PlusValues);
            }
            AllyViews.Add(Ally5_View);
        }
        if (_type == EntityUnitType.A4)
        {
            Ally Ally4 = new Ally();
            _baseAttribute = new BaseAttribute(1, 1, 1, 1, 1, 1, 1, 11, 1, 1, 1, 1, 1);

            Ally4.SetEntityData(new BaseAttribute(1, 1, 1, 1, 1, 1, 1, 11, 1, 1, 1, 1, 1), EntityUnitType.A4, WeaponType.WIZARD);
            var Ally4_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.A4, null, PosSpawAlly.transform.position, PosSpawAlly.transform.rotation);
            Ally4_View.SetData(Ally4);
            if (UpgradedAttribute.ContainsKey(_type))
            {
                BaseAttribute _PlusValues = UpgradedAttribute[_type];
                Ally4.ApplyAttributeUpgradeWhenPlay(_PlusValues);
            }
            AllyViews.Add(Ally4_View);
        }
        if (_type == EntityUnitType.A3)
        {
            Ally Ally3 = new Ally();
            _baseAttribute = new BaseAttribute(1, 1, 1, 1, 1, 1, 1, 11, 1, 1, 1, 1, 1);

            Ally3.SetEntityData(new BaseAttribute(1, 1, 1, 1, 1, 1, 1, 11, 1, 1, 1, 1, 1), EntityUnitType.A3, WeaponType.BLADE);
            var Ally3_View = EntityInitSystem.Instance.InitEntity(EntityUnitType.A3, null, PosSpawAlly.transform.position, PosSpawAlly.transform.rotation);
            Ally3_View.SetData(Ally3);
            if (UpgradedAttribute.ContainsKey(_type))
            {
                BaseAttribute _PlusValues = UpgradedAttribute[_type];
                Ally3.ApplyAttributeUpgradeWhenPlay(_PlusValues);
            }
            AllyViews.Add(Ally3_View);
        }
        if (!UpgradedAttribute.ContainsKey(_type)) {
            UpgradedAttribute.Add(_type, _baseAttribute);
        }
        CurrentCountAlly++;
        AllyChangeCount();
    }
    
    public void MoveAllyToReadyPos() {
        float xOffset = 0.5f; 
        float yOffsetA2 = 0.5f; 
        float yOffsetA1 = 1;
        float yOffsetA5 = 0;

        Vector3 rootPos = PosSpawAlly.transform.position;
        List<EntityView> a1Entities = new List<EntityView>();
        List<EntityView> a2Entities = new List<EntityView>();
        List<EntityView> a5Entities = new List<EntityView>();
        // Phân loại các đối tượng vào danh sách tương ứng
        foreach (EntityView entityView in AllyViews)
        {
            if (entityView.EntityType == EntityUnitType.A1 || entityView.EntityType == EntityUnitType.A3)
            {
                a1Entities.Add(entityView);
            }
            else if (entityView.EntityType == EntityUnitType.A2 || entityView.EntityType == EntityUnitType.A4)
            {
                a2Entities.Add(entityView);
            }
            else if (entityView.EntityType == EntityUnitType.A5)
            {
                a5Entities.Add(entityView);
            }
        }

        // Tính toán vị trí cho A2 đối xứng
        for (int i = 0; i < a2Entities.Count; i++)
        {
            Vector3 position = new Vector3(
                rootPos.x - xOffset * (a2Entities.Count - 1) / 2 + i * xOffset,
                rootPos.y + yOffsetA2
            );
            a2Entities[i].transform.position = position;
        }

        // Tính toán vị trí cho A1 đối xứng
        for (int i = 0; i < a1Entities.Count; i++)
        {
            Vector3 position = new Vector3(
                rootPos.x - xOffset * (a1Entities.Count - 1) / 2 + i * xOffset,
                rootPos.y + yOffsetA1
            );
            a1Entities[i].transform.position = position;
        }

        for (int i = 0; i < a5Entities.Count; i++)
        {
            Vector3 position = new Vector3(
                rootPos.x - xOffset * (a5Entities.Count - 1) / 2 + i * xOffset,
                rootPos.y + yOffsetA5
            );
            a5Entities[i].transform.position = position;
        }
    }

    public void ClearDataLevel() {
        foreach (var temp in AllEnemy) {
            if (temp != null) {
                Destroy(temp.gameObject);
            }
        }
        BrickAndEnemy.Clear();
        AllEntityView.Clear();
        AllEnemy.Clear();
        //AllAlly.Clear();
        //AllAlly = new List<EntityView>();
        AllEnemy = new List<EntityView>();
        AllEntityView = new List<EntityView>();
        BrickAndEnemy = new List<MonoBehaviour>();
    }

    public void Lose() {
        foreach (var temp in AllyViews)
        {
            if (temp != null)
            {
                Destroy(temp.gameObject);
            }
        }
        UpgradedAttribute.Clear();
        UpgradedAttribute = new Dictionary<EntityUnitType, BaseAttribute>();
        AllyViews.Clear();
        AllyViews = new List<MonoBehaviour>();
        UserData.Money = 0;
        PopupControl.Instance.ShowLose();
        ClearDataLevel();
        GameController.Instance.ClearOldLevel();
        EventDispatcher.Instance.Dispatch(EventName.LOCK_NEWUNIT,EntityUnitType.A2);
        EventDispatcher.Instance.Dispatch(EventName.LOCK_NEWUNIT, EntityUnitType.A5);
        PopupControl.Instance.ResetUpgrade();
    }
    public void NextLevel() {
        ClearDataLevel();
        MoveAllyToReadyPos();
    }
    public void AllyDead() {
        CurrentCountAlly--;
        AllyChangeCount();
        if (CurrentCountAlly <= 0) {
            Lose();
        }

    }
   
    private void AllyChangeCount() {
        EventDispatcher.Instance.Dispatch(EventName.ALLY_CHANGE, CurrentCountAlly);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            MoveAllyToReadyPos();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Lose();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            SpawAllyByType(EntityUnitType.A3);
        }
    }
#endif
    private void OnApplicationQuit()
    {
        UserData.Money = 0;
    }
}
