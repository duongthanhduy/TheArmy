using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayScreen : MonoBehaviour
{
    [SerializeField] TMP_Text txtMoney;
    [SerializeField] Button btnCheat;
    [SerializeField] Button btnUnlockNewUnit;
    [SerializeField] List<UIControlUnit> UIControlUnit;
    [SerializeField] TMP_Text txtCountAlly;
    [SerializeField] Button btnUpSpeed;
    void Start()
    {
        EventDispatcher.Instance.AddListener(EventName.MONEY_CHANGE, UpdateUIMoney);
        EventDispatcher.Instance.AddListener(EventName.UNLOCK_NEWUNIT, UnlockNewUnit);
        EventDispatcher.Instance.AddListener(EventName.ALLY_CHANGE,AllyChangeCount);
        EventDispatcher.Instance.AddListener(EventName.LOCK_NEWUNIT, LockNewUnit);
        btnCheat.onClick.RemoveAllListeners();
        btnCheat.onClick.AddListener(OnClickbtnCheat);

        btnUnlockNewUnit.onClick.RemoveAllListeners();
        btnUnlockNewUnit.onClick.AddListener(OnClickbtnUnlockNewUnit);

        btnUpSpeed.onClick.RemoveAllListeners();
        btnUpSpeed.onClick.AddListener(OnbtnUpSpeedClick);
        UnlockNewUnit(EventName.NONE, EntityUnitType.A1);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener(EventName.MONEY_CHANGE, UpdateUIMoney);
        EventDispatcher.Instance.RemoveListener(EventName.UNLOCK_NEWUNIT, UnlockNewUnit);
        EventDispatcher.Instance.RemoveListener(EventName.ALLY_CHANGE, AllyChangeCount);
        EventDispatcher.Instance.RemoveListener(EventName.LOCK_NEWUNIT, LockNewUnit);
    }


    private void UpdateUIMoney(EventName evn, object ob) {
        txtMoney.text = $"${UserData.Money}";
    }

    private void OnClickbtnCheat() {
        UserData.Money += 90;    
    }

    private void OnClickbtnUnlockNewUnit()
    {
        if (UserData.Money >= GameVariableConfig.PriceUnlockNewUnit) {
            UserData.Money -= GameVariableConfig.PriceUnlockNewUnit;
            PopupControl.Instance.ShowLuckyUnitPopup();

        }

    }

    private void UnlockNewUnit(EventName evn, object ob) {
        EntityUnitType newtype = (EntityUnitType)ob;
        var newunit = UIControlUnit.Find(x=>x._unlock == false && x.unitType == newtype);
        if (newunit != null) {
            newunit.Unlock();
            newunit.gameObject.SetActive(true);
        }
    }
    public void LockNewUnit(EventName evn, object ob) {
        EntityUnitType newtype = (EntityUnitType)ob;
        var newunit = UIControlUnit.Find(x => x._unlock == true && x.unitType == newtype);
        if (newunit != null)
        {
            newunit.Lock();
            newunit.gameObject.SetActive(false);
        }
    }
    private void AllyChangeCount(EventName evn, object ob) {
        int currentCountAlly = (int)ob;
        txtCountAlly.text = $"{currentCountAlly}/{GameVariableConfig.MaxAllyEntity}";
    }

    private void OnbtnUpSpeedClick() {
        Time.timeScale = 2f;
    }

    public void ResetUpgrade() {
        for (int i = 0; i < UIControlUnit.Count;i++) {
            UIControlUnit[i].ResetUpgrade();
        } 
    }
   
    public void UpdateUICountEntity(EntityUnitType _type,int count) {
        UIControlUnit.Find(x => x.unitType == _type).UpdateCountEntity(count);
    }
    public void SetBaseValueUIUpgrade(EntityUnitType _type, float _power, float _health, float _atkSpd) {
        var UIUnit = UIControlUnit.Find(x => x.unitType == _type);
        if (UIUnit != null)
        {
            UIUnit.SetBaseValueUpgrade(_power, _health, _atkSpd);

        }
    }
}
