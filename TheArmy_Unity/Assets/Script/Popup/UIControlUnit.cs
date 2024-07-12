using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIControlUnit : MonoBehaviour
{
    public bool _unlock { get;private set;} = false;
    public EntityUnitType unitType;
    [SerializeField] Image imgIconEntity;
    //[SerializeField] Button btnSpawnUnit,btnPower,btnHealth,btnAtkSpd;
    [Header("UNITS")]
    [SerializeField] Button btnSpawnUnit;
    [SerializeField] TMP_Text txtCountEntity;
    [SerializeField] TMP_Text txtPriceEntity;
    [Header("POWER")]
    [SerializeField] Button btnPower;
    [SerializeField] TMP_Text txtCurrentPower;
    [SerializeField] TMP_Text txtPricePower;
    [Header("HEAL")]
    [SerializeField] Button  btnHealth;
    [SerializeField] TMP_Text txtCurrenthealth;
    [SerializeField] TMP_Text txtPriceHealth;
    [Header("ATK SPD")]
    [SerializeField] Button btnAtkSpd;
    [SerializeField] TMP_Text txtCurrentATKSPD;
    [SerializeField] TMP_Text txtPriceATKSPD;

    private float currentUnit = 0;
    private int currentPriceUnit = 1;

    private float currentPower = 0;
    private int currentPricePower = 1;

    private float currentHealth = 0;
    private int currentPriceHealth = 1;

    private float currentAtkSpd = 0;
    private int currentPriceAtkSpd = 1;

    float valueUpgradePower = 0.5f;
    float valueUpgradeHealth = 0.5f;
    float valueUpgradeAtkSpd = 0.1f;
    public void SetEntityType(EntityUnitType _type) {
        unitType = _type;
    }

    private void Start()
    {
        //SetEntityType(EntityUnitType.A1);
        btnSpawnUnit.onClick.RemoveAllListeners();
        btnPower.onClick.RemoveAllListeners();
        btnHealth.onClick.RemoveAllListeners();
        btnAtkSpd.onClick.RemoveAllListeners();

        btnSpawnUnit.onClick.AddListener(OnclickbtnSpawnUnit);
        btnPower.onClick.AddListener(OnclickbtnPower);
        btnHealth.onClick.AddListener(OnclickbtnHealth);
        btnAtkSpd.onClick.AddListener(OnClickbtnAtkspd);
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI() {
        txtCountEntity.text = currentUnit.ToString();
        txtPriceEntity.text = $"{currentPriceUnit}$";

        txtCurrentPower.text = currentPower.ToString();
        txtPricePower.text = $"{currentPricePower}$";

        txtCurrenthealth.text = $"{currentHealth}";
        txtPriceHealth.text = $"{currentPriceHealth}$";

        txtCurrentATKSPD.text = $"{currentAtkSpd}";
        txtPriceATKSPD.text = $"{currentPriceAtkSpd}$";
    }

    private void OnclickbtnSpawnUnit() {
        if (UserData.Money >= currentPriceUnit && GameController.Instance.GetCurrentAllyCount() < GameVariableConfig.MaxAllyEntity) {
            UserData.Money -= currentPriceUnit;
            GameController.Instance.InitEntityByType(unitType);
            currentUnit ++;
            currentPriceUnit += 2;
        }
        txtCountEntity.text = currentUnit.ToString();
        txtPriceEntity.text = $"{currentPriceUnit}$";
    }

    private void OnclickbtnPower() {
        if (UserData.Money >= currentPricePower)
        {
            UserData.Money -= currentPricePower;

            currentPower += valueUpgradePower;
            currentPricePower += 2;
            EventDispatcher.Instance.Dispatch(EventName.UPGRADE_POWER, new Tuple<EntityUnitType, float>(unitType, valueUpgradePower));
            GameController.Instance.RefrestDataUpgradeWhenPlay(unitType, AttributeType.power, valueUpgradePower);
        }
        txtCurrentPower.text = currentPower.ToString();
        txtPricePower.text = $"{currentPricePower}$";
    }
    private void OnclickbtnHealth() {
        if (UserData.Money >= currentPriceHealth)
        {
            UserData.Money -= currentPriceHealth;
            
            currentHealth += valueUpgradeHealth;
            currentPriceHealth += 2;
            EventDispatcher.Instance.Dispatch(EventName.UPGRADE_HEALTH,new Tuple<EntityUnitType,float>(unitType, valueUpgradeHealth));
            GameController.Instance.RefrestDataUpgradeWhenPlay(unitType,  AttributeType.health , valueUpgradeHealth);
        }
        txtCurrenthealth.text = $"{currentHealth}";
        txtPriceHealth.text = $"{currentPriceHealth}$";
    }
    private void OnClickbtnAtkspd() {
        if (UserData.Money >= currentPriceAtkSpd)
        {
            UserData.Money -= currentPriceAtkSpd;

            currentAtkSpd += valueUpgradeAtkSpd;
            currentPriceAtkSpd += 2;
            EventDispatcher.Instance.Dispatch(EventName.UPGRADE_ATKSPD, new Tuple<EntityUnitType, float>(unitType, valueUpgradeAtkSpd));
            GameController.Instance.RefrestDataUpgradeWhenPlay(unitType, AttributeType.atkspeed, valueUpgradeAtkSpd);
        }
        txtCurrentATKSPD.text = $"{currentAtkSpd}";
        txtPriceATKSPD.text = $"{currentPriceAtkSpd}$";
    }
    public void Unlock() {
        _unlock = true;
    }
    public void Lock() {
        _unlock = false;    
    }
    public void ResetUpgrade() {
        currentUnit = 0;
         currentPriceUnit = 1;

        currentPower = 0;
         currentPricePower = 1;

         currentHealth = 0;
         currentPriceHealth = 1;

        currentAtkSpd = 0;
        currentPriceAtkSpd = 1;
        UpdateUI();
    }
    public void SetBaseValueUpgrade(float _power,float _health,float _atkSpd) {
        currentPower = _power;
        currentHealth = _health;
        currentAtkSpd = _atkSpd;
        UpdateUI();
    }

    public void UpdateCountEntity(int _count) {
        currentUnit = _count;
        txtCountEntity.text = currentUnit.ToString();
    }
}
