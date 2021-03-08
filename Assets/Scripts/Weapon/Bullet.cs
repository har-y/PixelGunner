using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private bool _playerBullet;
    [SerializeField] private bool _enemyBullet;


    [Header("Bullet - Damage")]
    [SerializeField] private int _bulletDamage;

    [Header("Bullet - Visual Effects")]
    [SerializeField] private GameObject _bulletHitEffect;
    private GameObject _effectSlot;

    [Header("Bullet - Movement")]
    [SerializeField] private float _moveSpeed;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _effectSlot = GameObject.FindGameObjectWithTag("Misc");

        _direction = PlayerController.instance.transform.position - transform.position;
        _direction.Normalize();
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BulletHitObject();

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().EnemyDamage(_bulletDamage);
        }

        if (other.tag == "Player")
        {

        }
    }

    private void BulletHitObject()
    {
        if (_playerBullet)
        {
            GameObject bulletEffect = Instantiate(_bulletHitEffect, transform.position, transform.rotation);
            bulletEffect.transform.parent = _effectSlot.transform;
        }

        Destroy(gameObject);
    }
}
