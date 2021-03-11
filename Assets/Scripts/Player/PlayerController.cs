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
    [SerializeField] private float _moveSpeed;
    private Vector2 _moveInput;

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

        _bulletSlot = GameObject.FindGameObjectWithTag("Misc");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }

    private void PlayerControl()
    {
        PlayerMove();
        PlayerWeaponMove();
        PlayerShoot();
        PlayerAnimation();
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
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        _moveInput.Normalize();

        _rigidbody2D.velocity = _moveInput * _moveSpeed;
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
}
