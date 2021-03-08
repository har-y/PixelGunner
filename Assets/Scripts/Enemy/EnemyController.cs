using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy")]
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    [Header("Enemy - Health")]
    [SerializeField] private int _enemyHealth;

    [Header("Enemy - Movement")]
    [SerializeField] private float _moveSpeed;
    private Vector3 _moveDirection;

    [Header("Enemy - Enemy vs. Player")]
    [SerializeField] private float _enemyRange;
    [SerializeField] private float _enemyShootRange;
    [SerializeField] private bool _shootEnemy;

    [Header("Enemy - Bullet")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _bulletDelay;
    private float _bulletCounter;
    private GameObject _bulletSlot;

    [Header("Enemy - Visual Effects")]
    [SerializeField] private GameObject _enemyHitEffect;
    [SerializeField] private GameObject _enemyDeathEffect;
    private GameObject _effectSlot;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _effectSlot = GameObject.FindGameObjectWithTag("Misc");
        _bulletSlot = GameObject.FindGameObjectWithTag("Misc");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        EnemyAnimation();

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _enemyShootRange)
        {
            if (_shootEnemy)
            {
                _bulletCounter -= Time.deltaTime;

                if (_bulletCounter <= 0)
                {
                    _bulletCounter = _bulletDelay;

                    GameObject bullet = Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
                    bullet.transform.parent = _bulletSlot.transform;
                }
            }
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

    public void EnemyDamage(int damage)
    {
        _enemyHealth -= damage;

        GameObject hitEffect = Instantiate(_enemyHitEffect, transform.position, transform.rotation);
        hitEffect.transform.parent = _effectSlot.transform;

        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);

            Instantiate(_enemyDeathEffect, transform.position, transform.rotation);
        }
    }
}
