using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UtilityManager : Singleton<UtilityManager>
{
    public UnityEvent BossEvent = new UnityEvent();
    public float MaxBossHealth;
    public float BossHealth;
}

