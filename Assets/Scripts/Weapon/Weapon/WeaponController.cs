using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Player - Weapon")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private int _bulletSound;
    private GameObject _bulletSlot;

    [Header("Player - Reload")]
    [SerializeField] private float _bulletDelay;
    private float _bulletCounter;


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
}
