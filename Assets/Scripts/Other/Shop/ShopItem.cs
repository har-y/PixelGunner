using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [Header("Shop Item")]
    [SerializeField] private GameObject _purchaseText;

    [Header("Shop - Purchase")]
    private bool _buyArea;
    [SerializeField] private bool _healthRestore;
    [SerializeField] private bool _healthUpgrade;
    [SerializeField] private bool _weaponChange;

    [SerializeField] private int _itemPrice;


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
                    LevelManager.instance.SpendCoins(_itemPrice);

                    if (_healthRestore)
                    {
                        PlayerHealthController.instance.PlayerHeal(PlayerHealthController.instance.MaxHealth);
                    }

                }
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
