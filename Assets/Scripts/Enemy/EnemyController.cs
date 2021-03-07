using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy")]
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    [Header("Enemy Movement")]
    [SerializeField] private float _moveSpeed;
    private Vector3 _moveDirection;

    [Header("Enemy vs. Player")]
    [SerializeField] private float _enemyRange;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        EnemyAnimation();
    }

    private void EnemyAnimation()
    {
        if (_moveDirection != Vector3.zero)
        {
            _animator.SetBool("isMove", true);
        }
        else
        {
            _animator.SetBool("isMove", false);
        }
    }

    private void EnemyMove()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _enemyRange)
        {
            _moveDirection = PlayerController.instance.transform.position - transform.position;
            _moveDirection.Normalize();

        }
        else
        {
            _moveDirection = Vector3.zero;
        }

        _rigidbody2D.velocity = _moveDirection * _moveSpeed;
    }
}
