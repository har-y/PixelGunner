using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [Header("Player Health")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [Header("Player Hurt")]
    [SerializeField] private float _invincibleTime;
    private float _invincibleCounter;
    private Color _bodyDefaultColor;
    private Color _bodyInvincibleColor;
    private Color _handDefaultColor;
    private Color _handInvincibleColor;
    private Color _gunDefaultColor;
    private Color _gunInvincibleColor;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;

        _bodyDefaultColor = PlayerController.instance.PlayerBodySprite.color;
        _bodyInvincibleColor = new Color(PlayerController.instance.PlayerBodySprite.color.r, PlayerController.instance.PlayerBodySprite.color.g, PlayerController.instance.PlayerBodySprite.color.b, 0.5f);

        _handDefaultColor = PlayerController.instance.PlayerHandSprite.color; 
        _handInvincibleColor = new Color(PlayerController.instance.PlayerHandSprite.color.r, PlayerController.instance.PlayerHandSprite.color.g, PlayerController.instance.PlayerHandSprite.color.b, 0.5f);

        _gunDefaultColor = PlayerController.instance.PlayerGunSprite.color;
        _gunInvincibleColor = new Color(PlayerController.instance.PlayerGunSprite.color.r, PlayerController.instance.PlayerGunSprite.color.g, PlayerController.instance.PlayerGunSprite.color.b, 0.5f);
    }


    // Update is called once per frame
    void Update()
    {
        PlayerInvincible();
    }

    private void PlayerInvincible()
    {
        if (_invincibleCounter >= 0)
        {
            _invincibleCounter -= Time.deltaTime;
        }
        else
        {
            PlayerController.instance.PlayerBodySprite.color = _bodyDefaultColor;
            PlayerController.instance.PlayerHandSprite.color = _handDefaultColor;
            PlayerController.instance.PlayerGunSprite.color = _gunDefaultColor;
        }
    }

    public void PlayerDamage()
    {
        if (_invincibleCounter <= 0)
        {
            _currentHealth--;

            _invincibleCounter = _invincibleTime;

            PlayerController.instance.PlayerBodySprite.color = _bodyInvincibleColor;
            PlayerController.instance.PlayerHandSprite.color = _handInvincibleColor;
            PlayerController.instance.PlayerGunSprite.color = _gunInvincibleColor;

            if (_currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.LostScreenOn();
            }
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
