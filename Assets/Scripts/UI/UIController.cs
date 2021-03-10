using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("UI")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Text _healthText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _healthSlider.maxValue = PlayerHealthController.instance.MaxHealth;
        _healthSlider.value = PlayerHealthController.instance.CurrentHealth;
        _healthText.text = PlayerHealthController.instance.CurrentHealth.ToString() + " " + "/" + " " + PlayerHealthController.instance.MaxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _healthSlider.value = PlayerHealthController.instance.CurrentHealth;
        _healthText.text = PlayerHealthController.instance.CurrentHealth.ToString() + " " + "/" + " " + PlayerHealthController.instance.MaxHealth.ToString();
    }
}
