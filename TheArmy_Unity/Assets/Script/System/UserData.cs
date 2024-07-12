using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserData
{
    private const string MONEY = "MONEY";
    private const string STAR = "STAR";

    

    public static int Money
    {
        get { return PlayerPrefs.GetInt(MONEY, 0); }
        set
        {
            int newValue = Mathf.Clamp(value, 0, int.MaxValue);
            PlayerPrefs.SetInt(MONEY, newValue);
            EventDispatcher.Instance.Dispatch(EventName.MONEY_CHANGE, null);
        }
    }
    public static int Star
    {
        get { return PlayerPrefs.GetInt(STAR, 0); }
        set
        {
            int newValue = Mathf.Clamp(value, 0, int.MaxValue);
            PlayerPrefs.SetInt(STAR, newValue);
            EventDispatcher.Instance.Dispatch(EventName.STAR_CHANGE, null);
        }
    }

   
}