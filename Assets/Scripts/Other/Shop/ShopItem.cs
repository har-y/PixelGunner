using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [Header("Shop Item")]
    [SerializeField] private GameObject _purchaseText;

    [Header("Shop - Purchase")]
    private bool _buyArea;
    private bool _boughtItem;
    [SerializeField] private bool _healthRestore;
    [SerializeField] private bool _healthUpgrade;
    [SerializeField] private bool _weaponChange;

    [SerializeField] private int _itemPrice;
    [SerializeField] private int _healthUpgradeValue;
    [SerializeField] private int _buySound;
    [SerializeField] private int _notBuySound;

    // Start is called before the first frame update
    void Start()
    {
        _purchaseText.SetActive(false);
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
