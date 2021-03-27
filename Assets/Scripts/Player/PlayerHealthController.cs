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
    [SerializeField] private int _playerHurtSound;
    [SerializeField] private int _playerDeathSound;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;

        _bodyDefaultColor = PlayerController.instance.PlayerBodySprite.color;
        _bodyInvincibleColor = new Color(_bodyDefaultColor.r, _bodyDefaultColor.g, _bodyDefaultColor.b, 0.5f);

        _handDefaultColor = PlayerController.instance.PlayerHandSprite.color; 
        _handInvincibleColor = new Color(_handDefaultColor.r, _handDefaultColor.g, _handDefaultColor.b, 0.5f);

        _gunDefaultColor = PlayerController.instance.PlayerGunSprite.color;
        _gunInvincibleColor = new Color(_gunDefaultColor.r, _gunDefaultColor.g, _gunDefaultColor.b, 0.5f);
    }


    // Update is called once per frame
    void Update()
    {
        PlayerInvincible();
    }

    private void PlayerInvincible()
    {
        if (_invincibleCounter > 0)
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

            AudioManager.instance.PlaySoundClip(_playerHurtSound);

            ActivateInvincible(_invincibleTime);

            if (_currentHealth <= 0)
            {
                AudioManager.instance.PlaySoundClip(_playerDeathSound);

                PlayerController.instance.gameObject.SetActive(false);

                UIController.instance.LostScreenOn();

                AudioManager.instance.PlayMusic(2);
            }
        }      
    }

    public void ActivateInvincible(float time)
    {
        _invincibleCounter = time;

        PlayerController.instance.PlayerBodySprite.color = _bodyInvincibleColor;
        PlayerController.instance.PlayerHandSprite.color = _handInvincibleColor;
        PlayerController.instance.PlayerGunSprite.color = _gunInvincibleColor;
    }

    public void PlayerHeal(int value)
    {
        _currentHealth += value;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
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
    }
}
