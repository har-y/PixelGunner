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
    private Vector3 _shiftDirection;

    [Header("Enemy - Enemy vs. Player")]
    [Header("Chase")]
    [SerializeField] private bool _chaseEnemy;
    [SerializeField] private float _chaseRange;
    [Header("Shoot")]
    [SerializeField] private bool _shootEnemy;
    [SerializeField] private float _shootRange;
    [Header("Run")]
    [SerializeField] private bool _runEnemy;
    [SerializeField] private float _runRange;
    [Header("Shift")]
    [SerializeField] private bool _shiftEnemy;
    [SerializeField] private float _shiftTime;
    [SerializeField] private float _pauseTime;
    private float _shiftCounter;
    private float _pauseCounter;
    [Header("Patrol")]
    [SerializeField] private bool _patrolEnemy;
    [SerializeField] private Transform[] _patrolPoints;
    private int _currentPoint;
    private GameObject _pointSlot;

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

    [Header("Enemy - Drop")]
    [SerializeField] private bool _drop;
    [SerializeField] GameObject[] _dropPrefab;
    [SerializeField] private float _dropChance;
    private float _dropValue;
    private int _dropItem;
    private GameObject _dropSlot;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _enemyDefaultMaterial = _enemy.material;

        _dropValue = Random.Range(0f, 100f);

        _effectSlot = GameObject.FindGameObjectWithTag("Misc");
        _bulletSlot = GameObject.FindGameObjectWithTag("Misc");
        _pointSlot = GameObject.FindGameObjectWithTag("Misc");
        _dropSlot = GameObject.FindGameObjectWithTag("Misc");

        EnemyShift();
        PatrolPoints();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyControl();
    }

    private void EnemyControl()
    {
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
        _moveDirection = Vector3.zero;

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= _chaseRange && _chaseEnemy)
        {
            _moveDirection = PlayerController.instance.transform.position - transform.position;
        }
        else
        {       
            if (_shiftEnemy)
            {
                if (_shiftCounter > 0)
                {
                    _shiftCounter -= Time.deltaTime;

                    _moveDirection = _shiftDirection;

                    if (_shiftCounter <= 0)
                    {
                        _pauseCounter = Random.Range(_pauseTime * 0.5f, _pauseTime * 1.25f);
                    }
                }

                if (_pauseCounter > 0)
                {
                    _pauseCounter -= Time.deltaTime;

                    if (_pauseCounter <= 0)
                    {
                        _shiftCounter = Random.Range(_shiftTime * 0.5f, _shiftTime * 1.25f);

                        _shiftDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                    }
                }
            }

            if (_patrolEnemy)
            {
                _moveDirection = _patrolPoints[_currentPoint].position - transform.position;

                if (Vector3.Distance(transform.position, _patrolPoints[_currentPoint].position) < 0.2f)
                {
                    _currentPoint = Random.Range(0, _patrolPoints.Length);

                    if (_currentPoint >= _patrolPoints.Length)
                    {
                        _currentPoint = 0;
                    }
                }
            }
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

            InstantiateDrop();

            Destroy(gameObject);            
        }
    }

    private void EnemyShift()
    {
        if (_shiftEnemy)
        {
            _pauseCounter = Random.Range(_pauseTime * 0.5f, _pauseTime * 1.25f);
        }
    }

    private IEnumerator EnemyMaterialCoroutine()
    {
        _enemy.material = _enemyHitMaterial;

        yield return new WaitForSeconds(0.25f);

        _enemy.material = _enemyDefaultMaterial;
    }

    private void PatrolPoints()
    {
        foreach (var item in _patrolPoints)
        {
            item.parent = _pointSlot.transform;
        }
    }
    private void InstantiateDrop()
    {
        if (_drop)
        {
            if (_dropValue <= _dropChance)
            {
                GameObject drop = Instantiate(_dropPrefab[_dropItem], transform.position, transform.rotation);
                drop.transform.parent = _dropSlot.transform;
            }
        }
    }
}
