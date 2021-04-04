using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Controller")]
    [SerializeField] private SpriteRenderer _weaponSpriteRenderer;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private int _bulletSound;
    private GameObject _bulletSlot;

    [Header("Weapon Controller - Reload")]
    [SerializeField] private float _bulletDelay;
    private float _bulletCounter;

    [Header("Weapon Controller - UI")]
    [SerializeField] string _weaponName;
    [SerializeField] Sprite _weaponSprite;
    [SerializeField] Sprite _weaponShopSprite;

    [Header("Weapon Controller - Shop")]
    [SerializeField] private int _weaponCost;

    // Start is called before the first frame update
    void Start()
    {
        _bulletSlot = GameObject.FindGameObjectWithTag("Misc");
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.instance.IsPause)
        {
            WeaponShoot();
        }
    }

    private void WeaponShoot()
    {
        if (PlayerController.instance.CanMove)
        {
            if (_bulletCounter > 0)
            {
                _bulletCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
                {
                    InstantiateBullet();
                }
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

    public SpriteRenderer WeaponSpriteRenderer
    {
        get
        {
            return _weaponSpriteRenderer;
        }
    }

    public Sprite WeaponSprite
    {
        get
        {
            return _weaponSprite;
        }
    }

    public string WeaponName
    {
        get
        {
            return _weaponName;
        }
    }

    public Sprite WeaponShopSprite
    {
        get
        {
            return _weaponShopSprite;
        }
    }

    public int WeaponCost
    {
        get
        {
            return _weaponCost;
        }
    }
}
