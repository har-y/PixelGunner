using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    private Rigidbody2D _rigidbody2D;

    [Header("Bullet - Damage")]
    [SerializeField] private int _bulletDamage;

    [Header("Bullet - Visual Effects")]
    [SerializeField] private GameObject _effectPrefab;
    private GameObject _effectSlot;

    [Header("Bullet - Movement")]
    [SerializeField] private float _moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _effectSlot = GameObject.FindGameObjectWithTag("Misc");
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        _rigidbody2D.velocity = transform.right * _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BulletHitObject();

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().EnemyDamage(_bulletDamage);
        }
    }

    private void BulletHitObject()
    {
        GameObject bulletEffect = Instantiate(_effectPrefab, transform.position, transform.rotation);
        bulletEffect.transform.parent = _effectSlot.transform;

        Destroy(gameObject);
    }
}
