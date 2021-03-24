using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy")]
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    [SerializeField] private SpriteRenderer _enemy;

    [Header("Enemy - Health")]
    [SerializeField] private int _enemyHealth;

    [Header("Enemy - Movement")]
    [SerializeField] private float _moveSpeed;
    private Vector3 _moveDirection;

    [Header("Enemy - Enemy vs. Player")]
    [SerializeField] private bool _chaseEnemy;
    [SerializeField] private float _chaseRange;
    [SerializeField] private bool _shootEnemy;
    [SerializeField] private float _shootRange;
    [SerializeField] private bool _runEnemy;
    [SerializeField] private float _runRange;


    [Header("Enemy - Bullet")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _bulletDelay;
    private float _bulletCounter;
    private GameObject _bulletSlot;

    [Header("Enemy - Effects")]
    private Material _enemyDefaultMaterial;
    [SerializeField] private Material _enemyHitMaterial;
    [SerializeField] private GameObject _enemyHitEffect;
    private GameObject _effectSlot;
    [SerializeField] private int _enemyShootSound;
    [SerializeField] private int _enemyHurtSound;
    [SerializeField] private int _enemyDeathSound;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _enemyDefaultMaterial = _enemy.material;

        _effectSlot = GameObject.FindGameObjectWithTag("Misc");
        _bulletSlot = GameObject.FindGameObjectWithTag("Misc");       
    }

    // Update is called once per frame
    void Update()
    {
        EnemyControl();
    }

    private void EnemyControl()
    {
        _rigidbody2D.velocity = Vector2.zero;

        if (_enemy.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            EnemyMove();
            EnemyShoot();
            EnemyAnimation();
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void EnemyMove()
    {
        _rigidbody2D.velocity = Vector2.zero;

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _chaseRange && _chaseEnemy)
        {
            _moveDirection = PlayerController.instance.transform.position - transform.position;
        }

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _runRange && _runEnemy)
        {
            _moveDirection = transform.position - PlayerController.instance.transform.position;
        }

        _moveDirection.Normalize();
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

    private void EnemyShoot()
    {
        if (_shootEnemy && (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _shootRange))
        {
            _bulletCounter -= Time.deltaTime;

            if (_bulletCounter <= 0)
            {
                _bulletCounter = _bulletDelay;

                AudioManager.instance.PlaySoundClip(_enemyShootSound);

                GameObject bullet = Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
                bullet.transform.parent = _bulletSlot.transform;
            }
        }
    }

    public void EnemyDamage(int damage)
    {
        _enemyHealth -= damage;

        AudioManager.instance.PlaySoundClip(_enemyHurtSound);

        StartCoroutine(EnemyMaterialCoroutine());

        GameObject hitEffect = Instantiate(_enemyHitEffect, transform.position, transform.rotation);
        hitEffect.transform.parent = _effectSlot.transform;

        if (_enemyHealth <= 0)
        {
            AudioManager.instance.PlaySoundClip(_enemyDeathSound);

            Destroy(gameObject);            
        }
    }

    private IEnumerator EnemyMaterialCoroutine()
    {
        _enemy.material = _enemyHitMaterial;

        yield return new WaitForSeconds(0.25f);

        _enemy.material = _enemyDefaultMaterial;
    }
}
