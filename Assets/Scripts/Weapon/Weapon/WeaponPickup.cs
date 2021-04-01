using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [Header("Weapon Pickup")]
    [SerializeField] private WeaponController _weapon;
    [SerializeField] private bool _isCollectable;
    [SerializeField] private float _collectableTime;
    [SerializeField] private int _pickupSound;
    private bool hasWeapon;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CollectableCoroutine(_collectableTime));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isCollectable)
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        hasWeapon = false;

        foreach (WeaponController item in PlayerController.instance.AvilableWeapons)
        {
            if (_weapon.WeaponName == item.WeaponName)
            {
                hasWeapon = true;
            }
        }

        if (!hasWeapon)
        {
            GameObject newGun = Instantiate(_weapon.gameObject, PlayerController.instance.WeaponTransform);
            PlayerController.instance.AvilableWeapons.Add(newGun.GetComponent<WeaponController>());
            PlayerController.instance.WeaponChange();
        }

        AudioManager.instance.PlaySoundClip(_pickupSound);
        Destroy(gameObject);
    }

    IEnumerator CollectableCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        _isCollectable = true;
    }
}
