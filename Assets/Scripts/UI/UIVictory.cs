using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIVictory : MonoBehaviour
{
    [Header("UI Victory")]
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private GameObject _anyKeyText;
    [SerializeField] private string _mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        _anyKeyText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        if (_waitTime > 0)
        {
            _waitTime -= Time.deltaTime;

            if (_waitTime <= 0)
            {
                _anyKeyText.SetActive(true);
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(_mainMenu);
            }
        }
    }
}
