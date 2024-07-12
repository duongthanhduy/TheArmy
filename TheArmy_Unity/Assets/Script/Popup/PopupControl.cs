using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupControl : MonoBehaviour
{
    public static PopupControl Instance;

    [SerializeField] LuckyUnitPopup LuckyUnitPopup;
    [SerializeField] LosePopup LosePopup;
    [SerializeField] GamePlayScreen GameplayScreen;
    private void Awake()
    {
        Instance = this;
    }

    public void ShowLuckyUnitPopup() {
        LuckyUnitPopup.gameObject.SetActive(true);
    }
    public void ShowLose() {
        LosePopup.gameObject.SetActive(true);
    }
    public void ResetUpgrade() {
        GameplayScreen.ResetUpgrade();
    }
    public void UpdateUICountEntity(EntityUnitType entityUnitType,int count) {
        GameplayScreen.UpdateUICountEntity(entityUnitType, count);
    }
    public void SetBaseValueUIUpgrade(EntityUnitType _type, float _power, float _health, float _atkSpd)
    {
        GameplayScreen.SetBaseValueUIUpgrade(_type, _power, _health, _atkSpd);
    }
}
