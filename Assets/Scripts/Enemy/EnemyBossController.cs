using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : MonoBehaviour
{
    public static EnemyBossController instance;

    [Header("Enemy Boss Controller")]
    [SerializeField] private SpriteRenderer _bossSpriteRenderer;
    private Rigidbody2D _rigidbody2D;    
    private Vector2 _direction;

    [Header("Enemy Boss Controller - Action")]
    [SerializeField] private EnemyBossAction[] _action;

    [Header("Enemy Boss Controller - Action - Values")]
    private int _currentAction;
    private float _actionCounter;
    private float _bulletCounter;

    [Header("Enemy Boss Controller - Health")]
    [SerializeField] private int _currentHealth;

    [Header("Enemy Boss Controller - Effect")]
    private Material _bossDefaultMaterial;
    [SerializeField] private Material _bossHitMaterial;
    [SerializeField] private GameObject _bossHitEffect;

    [Header("Enemy Boss Controller - Level Exit")]
    [SerializeField] private GameObject _levelExit;

    [Header("Enemy Boss Controller - Slot")]
    private GameObject _bulletSlot;
    private GameObject _effectSlot;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _actionCounter = _action[_currentAction].ActionTime;
        _bossDefaultMaterial = _bossSpriteRenderer.material;
        _levelExit.SetActive(false);

        _effectSlot = GameObject.FindGameObjectWithTag("Misc");
        _bulletSlot = GameObject.FindGameObjectWithTag("Misc");

        UIController.instance.BossName.text = "Eye Watch";
        UIController.instance.BossSlider.maxValue = _currentHealth;
        UIController.instance.BossSlider.value = _currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_actionCounter > 0)
        {
            _actionCounter -= Time.deltaTime;

            EnemyBossMove();

            EnemyBossShoot();
        }
        else
        {
            _currentAction++;

            if (_currentAction >= _action.Length)
            {
                _currentAction = 0;
            }

            _actionCounter = _action[_currentAction].ActionTime;
        }
    }

    public void EnemyBossDamageReceive(int value)
    {
        _currentHealth -= value;


        StartCoroutine(EnemyMaterialCoroutine());

        if (_currentHealth <= 0)
        {
            GameObject deathEffect = Instantiate(_bossHitEffect, transform.position, transform.rotation);
            deathEffect.transform.parent = _effectSlot.transform;

            gameObject.SetActive(false);

            if (Vector3.Distance(PlayerController.instance.transform.position, _levelExit.transform.position) > 2f)
            {
                _levelExit.SetActive(true);
            }

            UIController.instance.BossHealthBar.SetActive(false);
        }

        UIController.instance.BossSlider.value = _currentHealth;
    }

    private void EnemyBossShoot()
    {
        if (_action[_currentAction].ShootEnemy)
        {
            _bulletCounter -= Time.deltaTime;

            if (_bulletCounter <= 0)
            {
                _bulletCounter = _action[_currentAction].BulletDelay;

                foreach (Transform item in _action[_currentAction].WeaponPoints)
                {
                    GameObject newBullet = Instantiate(_action[_currentAction].BulletPrefab, item.position, item.rotation);
                    newBullet.transform.parent = _bulletSlot.transform;
                }
            }
        }
    }

    private void EnemyBossMove()
    {
        _direction = Vector2.zero;

        if (_action[_currentAction].MoveAction)
        {
            if (_action[_currentAction].ChaseAction)
            {
                _direction = PlayerController.instance.transform.position - transform.position;
                _direction.Normalize();
            }

            if (_action[_currentAction].PointMoveAction)
            {
                _direction = _action[_currentAction].TargetPointMove.position - transform.position;
                _direction.Normalize();
            }
        }

        _rigidbody2D.velocity = _direction * _action[_currentAction].MoveSpeed;
    }

    private IEnumerator EnemyMaterialCoroutine()
    {
        _bossSpriteRenderer.material = _bossHitMaterial;

        yield return new WaitForSeconds(0.25f);

        _bossSpriteRenderer.material = _bossDefaultMaterial;
    }

    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
    }
}
