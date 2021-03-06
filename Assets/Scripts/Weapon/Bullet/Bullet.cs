using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private bool _playerBullet;
    [SerializeField] private bool _enemyBullet;
    [SerializeField] private bool _bossBullet;


    [Header("Bullet - Damage")]
    [SerializeField] private int _bulletDamage;

    [Header("Bullet - Effects")]
    [SerializeField] private GameObject _bulletHitEffect;
    private GameObject _effectSlot;
    [SerializeField] private int _bulletSound;

    [Header("Bullet - Movement")]
    [SerializeField] private float _moveSpeed;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _effectSlot = GameObject.FindGameObjectWithTag("Misc");

        if (_enemyBullet)
        {
            _direction = PlayerController.instance.transform.position - transform.position;
            _direction.Normalize();
        }

        if (_bossBullet)
        {
            _direction = transform.right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        if (_playerBullet)
        {
            _rigidbody2D.velocity = transform.right * _moveSpeed;
        }
        else if (_enemyBullet)
        {
            transform.position += _direction * _moveSpeed * Time.deltaTime;
        }
        else if (_bossBullet)
        {
            transform.position += _direction * _moveSpeed * Time.deltaTime;

            if (!EnemyBossController.instance.gameObject.activeInHierarchy)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BulletHitObject();

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().EnemyDamage(_bulletDamage);
        }

        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.PlayerDamage();
        }

        if (other.CompareTag("Enemy - Boss"))
        {
            other.GetComponent<EnemyBossController>().EnemyBossDamageReceive(_bulletDamage);
        }
    }

    private void BulletHitObject()
    {
        if (_playerBullet)
        {
            AudioManager.instance.PlaySoundClip(_bulletSound);

            GameObject bulletEffect = Instantiate(_bulletHitEffect, transform.position, transform.rotation);
            bulletEffect.transform.parent = _effectSlot.transform;
        }

        Destroy(gameObject);
    }
}
