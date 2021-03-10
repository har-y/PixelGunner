using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [Header("Player Health")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDamage()
    {
        _currentHealth--;

        if (_currentHealth <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);
        }       
    }

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        
    }

    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
        }
    }
}
