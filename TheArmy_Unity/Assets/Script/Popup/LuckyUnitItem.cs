using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LuckyUnitItem : MonoBehaviour
{
    public EntityUnitType UnitType;
    public WeaponType WeaponType;
    [SerializeField] Image imgIconBody;
    [SerializeField] Image imgIconWeapon;
    [SerializeField] Image imgVisual;
    [SerializeField] Image imgSelectd;
    Tweener Tweener = null;
    private void Start()
    {
        UpdateIcon();
    }

    private void OnEnable()
    {
        imgSelectd.gameObject.SetActive(false);
    }

    private void UpdateIcon() {
        imgIconBody.sprite = InitIconSystem.Instance.GetIconBody(UnitType);
        imgIconWeapon.sprite = InitIconSystem.Instance.GetIconWeapon(WeaponType);
    }
    public void SetSelectd(bool _isSelect) {
        if (_isSelect) {
            if(Tweener != null) {
                Tweener.Kill();
            }
            imgVisual.color = Color.green;
            imgVisual.DOFade(1,0.001f);
            imgVisual.color = new Color32(0,255,0,255);
            imgSelectd.gameObject.SetActive(true);
        }
        
        imgVisual.gameObject.SetActive(_isSelect);
       
    }
    public void VisualSelectTemp() {
        //imgSelected.DOFade(1, 0.2f);
        if (Tweener != null) {
            Tweener.Kill();
        }
        imgVisual.color = Color.green;
        imgVisual.gameObject.SetActive(true);
        Tweener = imgVisual.DOFade(0,0.5f);
    }

    public void VisualStart() {
        imgVisual.gameObject.SetActive(true);
        imgVisual.DOColor(Color.green, 0.4f).OnComplete(() => {
            imgVisual.DOColor(Color.gray, 0.4f).SetDelay(0.2f);
        });  
    }
}
