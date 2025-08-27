using System;
using UnityEngine;

public class EnemyContext
{
    public Action<EnemyState> ChangeEnemyState;
    public AnimationManager animMan;
}
