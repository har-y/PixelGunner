using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBossSequence
{
    [Header("Enemy Boss Sequence")]

    [SerializeField] private EnemyBossAction[] _actions;
    [SerializeField] private int _endSequence;

    public EnemyBossAction[] EnemyBossAction
    {
        get
        {
            return _actions;
        }
    }

    public int EndSequence
    {
        get
        {
            return _endSequence;
        }
    }
}
