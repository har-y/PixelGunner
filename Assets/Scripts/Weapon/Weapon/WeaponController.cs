using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Player - Weapon")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _bulletDelay;
    [SerializeField] private int _bulletSound;
    private float _bulletCounter;
    private GameObject _bulletSlot;

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
            PlayerShoot();
        }
    }

    private void PlayerShoot()
    {
        if (PlayerController.instance.CanMove)
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
    }

    private void InstantiateBullet()
    {
        AudioManager.instance.PlaySoundClip(_bulletSound);

        GameObject bullet = Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
        bullet.transform.parent = _bulletSlot.transform;

        _bulletCounter = _bulletDelay;
    }
}
