using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : MonoBehaviour
{
    public static EnemyBossController instance;

    [Header("Enemy Boss Controller")]
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private EnemyBossAction[] _action;
    private Vector2 _direction;
    private int _currentAction;
    private float _actionCounter;
    private float _bulletCounter;

    private GameObject _bulletSlot;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _actionCounter = _action[_currentAction].ActionTime;

        _bulletSlot = GameObject.FindGameObjectWithTag("Misc");
    }

    // Update is called once per frame
    void Update()
    {
        if (_actionCounter > 0)
        {
            _actionCounter -= Time.deltaTime;

            EnemyBossMove();

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
}
