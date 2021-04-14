using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBossAction
{
    [Header("Enemy Boss Action")]

    [Header("Enemy Boss Action - Move")]
    [SerializeField] private bool _moveAction;
    [SerializeField] private bool _chaseAction;
    [SerializeField] private bool _pointMoveAction;

    [Header("Enemy Boss Action - Move - Values")]
    [SerializeField] private float _actionTime;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _targetPointMove;

    [Header("Enemy Boss Action - Shoot")]
    [SerializeField] private bool _shootEnemy;
    [SerializeField] private Transform[] _weaponPoints;
    [SerializeField] private GameObject _bulletPrefab;

    [Header("Enemy Boss Action - Shoot - Values")]
    [SerializeField] private float _bulletDelay;

    public bool MoveAction
    {
        get
        {
            return _moveAction;
        }
    }

    public bool ChaseAction
    {
        get
        {
            return _chaseAction;
        }
    }

    public bool PointMoveAction
    {
        get
        {
            return _pointMoveAction;
        }
    }

    public float ActionTime
    {
        get
        {
            return _actionTime;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }
    }

    public Transform TargetPointMove
    {
        get
        {
            return _targetPointMove;
        }
        set
        {
            _targetPointMove = value;
        }
    }

    public bool ShootEnemy
    {
        get
        {
            return _shootEnemy;
        }
    }

    public Transform[] WeaponPoints
    {
        get
        {
            return _weaponPoints;
        }
    }

    public GameObject BulletPrefab
    {
        get
        {
            return _bulletPrefab;
        }
    }

    public float BulletDelay
    {
        get
        {
            return _bulletDelay;
        }
    }
}
