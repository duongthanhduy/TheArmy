using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public override void SetAlly()
    {
        Ally = false;
    }
    public override void Dead()
    {
        base.Dead();
        UserData.Money += BaseHPInit;
    }


}
