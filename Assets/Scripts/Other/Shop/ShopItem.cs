using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [Header("Shop Item")]
    [SerializeField] private GameObject _purchaseText;

    [Header("Shop - Purchase")]
    private bool _buyArea;
    private bool _boughtItem;
    [SerializeField] private bool _healthRestore;
    [SerializeField] private bool _healthUpgrade;
    [SerializeField] private bool _weaponBuy;
    [SerializeField] private int _buySound;
    [SerializeField] private int _notBuySound;

    [Header("Shop - Price")]
    [SerializeField] private int _itemPrice;

    [Header("Shop - Health")]
    [SerializeField] private int _healthUpgradeValue;

    [Header("Shop - Weapon")]
    [SerializeField] private WeaponController[] _avilableWeapons;
    [SerializeField] private SpriteRenderer _weaponSpriteRenderer;
    [SerializeField] private Text _buyText;
    [SerializeField ]private string _infoText;
    private WeaponController _weapon;
    private bool _hasWeapon;

    // Start is called before the first frame update
    void Start()
    {
        _purchaseText.SetActive(false);

        if (_weaponBuy)
        {
            int selectWeapon = Random.Range(0, _avilableWeapons.Length);
            _weapon = _avilableWeapons[selectWeapon];
            _weaponSpriteRenderer.sprite = _weapon.WeaponShopSprite;
            _buyText.text = _infoText + "\n" + "-" + " " + _weapon.WeaponCost + " " + "GOLD" + " " + "-";
            _itemPrice = _weapon.WeaponCost;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_buyArea)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (LevelManager.instance.Coins >= _itemPrice)
                {
                    HealthRestore();
                    HealthUpgrade();
                    WeaponBuy();
                }

                if (_boughtItem)
                {
                    gameObject.SetActive(false);
                    _buyArea = false;

                    AudioManager.instance.PlaySoundClip(_buySound);
                }
                else
                {
                    AudioManager.instance.PlaySoundClip(_notBuySound);
                }
            }
        }
    }

    private void WeaponBuy()
    {
        if (_weaponBuy)
        {
            _hasWeapon = false;

            foreach (WeaponController item in PlayerController.instance.AvilableWeapons)
            {
                if (_weapon.WeaponName == item.WeaponName)
                {
                    _hasWeapon = true;
                }
            }

            if (!_hasWeapon)
            {
                GameObject newGun = Instantiate(_weapon.gameObject, PlayerController.instance.WeaponTransform);
                PlayerController.instance.AvilableWeapons.Add(newGun.GetComponent<WeaponController>());
                PlayerController.instance.WeaponChange();

                _boughtItem = true;
            }
        }
    }

    private void HealthUpgrade()
    {
        if (_healthUpgrade)
        {
            PlayerHealthController.instance.PlayerHealthUpgrade(_healthUpgradeValue);
            LevelManager.instance.SpendCoins(_itemPrice);

            _boughtItem = true;
        }
    }

    private void HealthRestore()
    {
        if (_healthRestore)
        {
            if (PlayerHealthController.instance.CurrentHealth < PlayerHealthController.instance.MaxHealth)
            {
                PlayerHealthController.instance.PlayerHeal(PlayerHealthController.instance.MaxHealth);
                LevelManager.instance.SpendCoins(_itemPrice);

                _boughtItem = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _purchaseText.SetActive(true);
            _buyArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _purchaseText.SetActive(false);
            _buyArea = false;
        }
    }
}
