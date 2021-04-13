using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBossAction
{
    [Header("Enemy Boss Action")]

    [Header("Move")]
    [SerializeField] private bool _moveAction;
    [SerializeField] private bool _chaseAction;
    [SerializeField] private bool _pointMoveAction;

    [Header("Move - Values")]
    [SerializeField] private float _actionTime;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _targetPointMove;

    [Header("Shoot")]
    [SerializeField] private bool _shootEnemy;
    [SerializeField] private Transform[] _weaponPoints;
    [SerializeField] private GameObject _bulletPrefab;

    [Header("Shoot - Values")]
    [SerializeField] private float _bulletDelay;
}
