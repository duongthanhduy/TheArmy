using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using UnityEngine.UI;

public class LuckyUnitPopup : MonoBehaviour
{
    public Ease ease;
    [SerializeField] Button btnSkip;
    [SerializeField] LuckyUnitItem[] luckyUnitItems;
    GameObject go;

    int resultRandom = 0;
    int result2 = 0;
    public int indexRotate = 0;
    private void Start()
    {
        btnSkip.onClick.RemoveAllListeners();
        btnSkip.onClick.AddListener(Hide);
    }

    private void OnEnable()
    {
        for (int i = 0; i < luckyUnitItems.Length;i++) {
            luckyUnitItems[i].SetSelectd(false);
        }
        resultRandom = UnityEngine.Random.Range(0, luckyUnitItems.Length - 1);
        //Debug.LogError($"RESULT: {luckyUnitItems[resultRandom].name}");
       

        for (int i = 0; i < 3; i++) {
            DOVirtual.DelayedCall(i * 1, () => {
                VisualStart();
            });
        }
        DOVirtual.DelayedCall(3, () => {
            RotateWheel();
        });
    }

    private void VisualStart() {
        for (int i = 0; i < luckyUnitItems.Length; i++)
        {
            luckyUnitItems[i].VisualStart();
        }
    }
  
    private void RotateWheel()
    {
        go = new GameObject();
        float totalRotation = 1080f + (360f / luckyUnitItems.Length) * resultRandom; // 3 vòng quay * 360 độ/vòng
        float duration = 4f;
        go. transform.DORotate(new Vector3(0f, 0f, totalRotation), duration, RotateMode.FastBeyond360)
            .SetEase(ease)
            .OnUpdate(() =>
            {
                // Tính toán góc quay hiện tại
                float currentAngle = go.transform.eulerAngles.z;
                int currentIndex = Mathf.FloorToInt(currentAngle / (360f / luckyUnitItems.Length));

                // Gọi hàm SetSelected cho phần tử tương ứng
                luckyUnitItems[currentIndex].VisualSelectTemp();
            })
            .OnComplete(() =>
            {
                // Xoay vòng xoay về góc ban đầu để chuẩn bị cho lần quay tiếp theo nếu cần
                //Debug.LogError($"{luckyUnitItems[resultRandom].name}");
                go. transform.eulerAngles = Vector3.zero;
                luckyUnitItems[resultRandom].SetSelectd(true);
                result2 = resultRandom -1;
                if (result2 < 0) {
                    result2 = resultRandom + 1;
                }
                luckyUnitItems[result2].SetSelectd(true);
                if (!IsInvoking(nameof(Hide))) {
                    Invoke(nameof(Hide),2);
                }
            });
    }


    private void OnDisable()
    {
        if(go != null) {
            Destroy(go);
        }
        EventDispatcher.Instance.Dispatch(EventName.UNLOCK_NEWUNIT, luckyUnitItems[resultRandom].UnitType);
        EventDispatcher.Instance.Dispatch(EventName.UNLOCK_NEWUNIT, luckyUnitItems[result2].UnitType);

    }

    private void Hide() {
        gameObject.SetActive(false);    
    }


}
