using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    private Rigidbody2D _rigidbody2D;

    [Header("Bullet Movement")]
    [SerializeField] private float _moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
}
