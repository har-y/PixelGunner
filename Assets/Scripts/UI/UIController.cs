using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("UI")]
    [SerializeField] private GameObject _lostScreen;

    [Header("UI - Player Health Bar")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Text _healthText;

    [Header("UI - Boss Health Bar")]
    [SerializeField] GameObject _bossHealthBar;
    [SerializeField] private Slider _bossHealthSlider;
    [SerializeField] private Text _bossText;

    [Header("UI - Coin")]
    [SerializeField] private Text _coinText;

    [Header("UI - Map")]
    [SerializeField] private GameObject _miniMap;
    [SerializeField] private GameObject _fullMap;

    [Header("UI - Weapon")]
    [SerializeField] private Image _currentWeapon;
    [SerializeField] private Text _weaponText;

    [Header("UI - Fade Screen")]
    [SerializeField] private Image _fadeScreen;
    [SerializeField] private float _fadeSpeed;
    [SerializeField] private bool _fadeOn;
    [SerializeField] private bool _fadeOff;

    [Header("UI - Pause Menu")]
    [SerializeField] private GameObject _pauseMenu;

    [Header("UI - Lost Screen")]
    [SerializeField] private string _mainMenu;
    [SerializeField] private string _newGame;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIUpdate();

        LostScreenOff();

        FadeOff();
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();
        FadeScreen();
    }

    private void FadeScreen()
    {
        if (_fadeOn)
        {
            FadeToBlack();
        }
        else if (_fadeOff)
        {
            FadeFromBlack();
        }
    }

    public void UIUpdate()
    {
        UIHealth();
        UICoin();
        UIWeapon();
    }

    private void UIHealth()
    {
        _healthSlider.maxValue = PlayerHealthController.instance.MaxHealth;
        _healthSlider.value = PlayerHealthController.instance.CurrentHealth;
        _healthText.text = PlayerHealthController.instance.CurrentHealth.ToString() + " " + "/" + " " + PlayerHealthController.instance.MaxHealth.ToString();
    }

    private void UICoin()
    {
        _coinText.text = LevelManager.instance.Coins.ToString();
    }

    private void UIWeapon()
    {
        _currentWeapon.sprite = PlayerController.instance.CurrentWeapon.WeaponSprite;
        _weaponText.text = PlayerController.instance.CurrentWeapon.WeaponName;
    }

    public void LostScreenOn()
    {
        _lostScreen.SetActive(true);
    }

    public void LostScreenOff()
    {
        _lostScreen.SetActive(false);
    }

    public void FadeToBlack()
    {
        _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 1f, _fadeSpeed * Time.deltaTime));
        if (_fadeScreen.color.a == 1f)
        {
            _fadeOn = false;
        }
    }

    public void FadeFromBlack()
    {
        _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 0f, _fadeSpeed * Time.deltaTime));
        if (_fadeScreen.color.a == 0f)
        {
            _fadeOff = false;
        }
    }

    public void FadeOn()
    {
        _fadeOn = true;
        _fadeOff = false;
    }

    public void FadeOff()
    {
        _fadeOn = false;
        _fadeOff = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;

        Destroy(PlayerController.instance.gameObject);

        SceneManager.LoadScene(_mainMenu);
    }

    public void NewGame()
    {
        Time.timeScale = 1;

        Destroy(PlayerController.instance.gameObject);

        SceneManager.LoadScene(_newGame);
    }

    public void Resume()
    {
        LevelManager.instance.Pause();
    }

    public GameObject PauseMenu
    {
        get
        {
            return _pauseMenu;
        }
    }

    public GameObject MiniMap
    {
        get
        {
            return _miniMap;
        }
    }

    public GameObject FullMap
    {
        get
        {
            return _fullMap;
        }
    }

    public Slider BossSlider
    {
        get
        {
            return _bossHealthSlider;
        }
        set
        {
            _bossHealthSlider = value;
        }
    }

    public Text BossName
    {
        get
        {
            return _bossText;
        }
        set
        {
            _bossText = value;
        }
    }

    public GameObject BossHealthBar
    {
        get
        {
            return _bossHealthBar;
        }
    }
}
