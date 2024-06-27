using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager 
{
    public static Action OnEnemyKilled;

    public static void SendEnemyKilled(){
        if(OnEnemyKilled != null) OnEnemyKilled.Invoke();
    }
}
