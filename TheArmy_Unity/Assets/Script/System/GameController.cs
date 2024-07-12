using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Level prefabLevel;
    private Level currentLevel;
    public GamePlayController gameplayControl;
    public Transform Gameplayscreen;
    void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
        InitLevel();
    }

    public void InitLevel() {
        ClearOldLevel();
        currentLevel = Instantiate(prefabLevel, Gameplayscreen);
        currentLevel.transform.localPosition = new Vector3(0, 6.13f, 0);
        gameplayControl.SetSpawEnemyPos(currentLevel.GetEnemySpawnPos()[0], currentLevel.GetEnemySpawnPos()[1], currentLevel.GetEnemySpawnPos()[2], currentLevel.GetEnemySpawnPos()[3]);
        gameplayControl.SpawnEnemy();
        if(gameplayControl.CurrentCountAlly == 0) {
            // first play
            gameplayControl.SpawAllyByType(EntityUnitType.A1);
            PopupControl.Instance.UpdateUICountEntity(EntityUnitType.A1,1);
        }
    }
    public void ClearOldLevel() {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
    }
    private void Win() {
        ClearOldLevel();
        NextLevel();
    }
    public void NextLevel() {
        ClearOldLevel();
        gameplayControl.NextLevel();
        InitLevel();
    }

    //private void Start()
    //{
    //    gameplayControl.SetSpawEnemyPos(currentLevel.GetEnemySpawnPos()[0], currentLevel.GetEnemySpawnPos()[1], currentLevel.GetEnemySpawnPos()[2], currentLevel.GetEnemySpawnPos()[3]);
    //    gameplayControl.SpawnEnemy();
    //}

    //public void SpawnAlly() {
    //    gameplayControl.SpawAlly();
    //}

    //public EntityView GetEntityNearest(EntityView _source) {
    //    return gameplayControl.GetEntityNearest(_source);
    //}
    public MonoBehaviour GetTargetNearest(MonoBehaviour _Source,bool _ally) {
         return gameplayControl.GetTargetNearest(_Source, _ally);   
    }
    public EntityView GetAllyLowHP(EntityView _source,bool _ally) {
        return gameplayControl.GetAllyLowHP(_source, _ally);
    }
    public void RemoveMono(MonoBehaviour _source) {
        gameplayControl.RemoveMono(_source);
    }
    public void RemoveEntity(EntityView _entity) {
        gameplayControl.RemoveEntity(_entity);
    }
    public void AddBrick(List<Brick> _brick) {
        gameplayControl.AddBrick(_brick);
    }
     
    public void InitEntityByType(EntityUnitType _type) {
        gameplayControl.SpawAllyByType(_type);
    }
    public void RefrestDataUpgradeWhenPlay(EntityUnitType _entityType, AttributeType _attributeType, float _newValue, Attribute _attribute = null) {
        gameplayControl.RefrestDataUpgradeWhenPlay(_entityType, _attributeType, _newValue);
    }
    public void AllyDead() {
        gameplayControl.AllyDead();
    }
    public int GetCurrentAllyCount() {
        return gameplayControl.CurrentCountAlly;    
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            NextLevel();
        }
    }
#endif
}
