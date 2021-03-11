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

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UISliderUpdate();
        LostScreenOff();
    }

    // Update is called once per frame
    void Update()
    {
        UISliderUpdate();
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
}
