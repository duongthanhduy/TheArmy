using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [SerializeField] Button btnReplay;

    private void Awake()
    {
        btnReplay.onClick.RemoveAllListeners();
        btnReplay.onClick.AddListener(OnClickbtnReplay);
    }

    private void OnClickbtnReplay() {
        gameObject.SetActive(false);
            GameController.Instance.InitLevel();
    }
}
