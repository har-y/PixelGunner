using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("UI")]
    [SerializeField] private GameObject _lostScreen;

    [Header("UI - Health Bar")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Text _healthText;

    [Header("UI - Fade Screen")]
    [SerializeField] private Image _fadeScreen;
    [SerializeField] private float _fadeSpeed;
    [SerializeField] private bool _fadeOn;
    [SerializeField] private bool _fadeOff;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UISliderUpdate();
        LostScreenOff();

        FadeOff();
    }

    // Update is called once per frame
    void Update()
    {
        UISliderUpdate();
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

    private void UISliderUpdate()
    {
        _healthSlider.maxValue = PlayerHealthController.instance.MaxHealth;
        _healthSlider.value = PlayerHealthController.instance.CurrentHealth;
        _healthText.text = PlayerHealthController.instance.CurrentHealth.ToString() + " " + "/" + " " + PlayerHealthController.instance.MaxHealth.ToString();
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
}
