using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInitSystem : MonoBehaviour
{
    public static EntityInitSystem Instance;
    [SerializeField] List<EntityView> entityViews;
    private void Awake()
    {
        Instance = this;
    }

    public EntityView InitEntity(EntityUnitType _entityType, Transform Parent, Vector3 _InitPos, Quaternion _InitRot)
    {
        return Instantiate(entityViews.Find(x => x.EntityType == _entityType),_InitPos, _InitRot);
    }
}
