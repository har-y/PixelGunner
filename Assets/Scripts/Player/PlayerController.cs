using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Player")]
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    [SerializeField] private SpriteRenderer _bodySpriteRenderer;
    [SerializeField] private SpriteRenderer _handSpriteRenderer;
    [SerializeField] private SpriteRenderer _gunSpriteRenderer;


    [Header("Player - Movement")]
    private Vector2 _moveInput;
    [SerializeField] private float _moveSpeed;
    private float _activeMoveSpeed;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashInvincible;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashCounter;
    private float _dashCooldownCounter;
    [SerializeField] private int _playerDashSound;
    [SerializeField] private bool _canMove;

    [Header("Player - Weapon")]
    [SerializeField] private Transform _weapon;
    private Camera _camera;
    private Vector3 _mousePosition;
    private Vector3 _screenPoint;
    private Vector2 _offset;
    private float _angle;

    [Header("Player - Bullet")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _bulletDelay;
    [SerializeField] private int _bulletSound;
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
        _animator = GetComponent<Animator>();
        _camera = Camera.main;

        _activeMoveSpeed = _moveSpeed;

        _bulletSlot = GameObject.FindGameObjectWithTag("Misc");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }

    private void PlayerControl()
    {
        if (!LevelManager.instance.IsPause)
        {
            PlayerMove();
            PlayerWeaponMove();
            PlayerShoot();
            PlayerAnimation();
        }
    }

    private void PlayerWeaponMove()
    {
        _mousePosition = Input.mousePosition;
        _screenPoint = _camera.WorldToScreenPoint(transform.localPosition);

        PlayerFlipPosition(_mousePosition, _screenPoint);

        _offset = new Vector2(_mousePosition.x - _screenPoint.x, _mousePosition.y - _screenPoint.y);
        _angle = Mathf.Atan2(_offset.y, _offset.x) * Mathf.Rad2Deg;

        _weapon.rotation = Quaternion.Euler(0f, 0f, _angle);
    }

    private void PlayerFlipPosition(Vector3 _mousePosition, Vector3 _screenPoint)
    {
        if (_mousePosition.x < _screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            _weapon.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            _weapon.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void PlayerMove()
    {
        if (_canMove)
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
            _moveInput.Normalize();

            _rigidbody2D.velocity = _moveInput * _activeMoveSpeed;

            PlayerDash();
        }
        else
        {
            PlayerStop();
        }
    }

    private void PlayerStop()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _moveInput = Vector2.zero;
    }

    private void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if (_dashCounter <= 0 && _dashCooldownCounter <= 0)
            {
                _activeMoveSpeed = _dashSpeed;
                _dashCounter = _dashTime;

                AudioManager.instance.PlaySoundClip(_playerDashSound);

                _animator.SetTrigger("isDash");

                PlayerHealthController.instance.ActivateInvincible(_dashInvincible);
            }
        }

        if (_dashCounter > 0)
        {
            _dashCounter -= Time.deltaTime;

            if (_dashCounter <= 0)
            {
                _activeMoveSpeed = _moveSpeed;
                _dashCooldownCounter = _dashCooldown;
            }
        }

        if (_dashCooldownCounter > 0)
        {
            _dashCooldownCounter -= Time.deltaTime;
        }
    }

    private void PlayerAnimation()
    {
        if (_moveInput != Vector2.zero)
        {
            _animator.SetBool("isMove", true);
        }
        else
        {
            _animator.SetBool("isMove", false);
        }
    }

    private void PlayerShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InstantiateBullet();
        }


        if (Input.GetMouseButton(0))
        {
            _bulletCounter -= Time.deltaTime;

            if (_bulletCounter <= 0)
            {
                InstantiateBullet();
            }
        }
    }

    private void InstantiateBullet()
    {
        AudioManager.instance.PlaySoundClip(_bulletSound);

        GameObject bullet = Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
        bullet.transform.parent = _bulletSlot.transform;

        _bulletCounter = _bulletDelay;
    }

    public SpriteRenderer PlayerBodySprite
    {
        get
        {
            return _bodySpriteRenderer;
        }
        set
        {
            _bodySpriteRenderer = value;
        }
    }

    public SpriteRenderer PlayerHandSprite
    {
        get
        {
            return _handSpriteRenderer;
        }
        set
        {
            _handSpriteRenderer = value;
        }
    }

    public SpriteRenderer PlayerGunSprite
    {
        get
        {
            return _gunSpriteRenderer;
        }
        set
        {
            _gunSpriteRenderer = value;
        }
    }

    public float DashCounter
    {
        get
        {
            return _dashCounter;
        }
    }

    public bool CanMove
    {
        get
        {
            return _canMove;
        }
        set
        {
            _canMove = value;
        }
    }
}
