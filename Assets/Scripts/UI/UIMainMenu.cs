using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [Header("UI Main Menu")]
    [SerializeField] private string _levelLoad;
    [SerializeField] private GameObject _resetPanel;
    [SerializeField] private PlayerSkinSelect[] _skinReset;

    // Start is called before the first frame update
    void Start()
    {
        _resetPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_levelLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        _resetPanel.SetActive(true);
    }

    public void ApplyReset()
    {
        _resetPanel.SetActive(false);

        foreach (PlayerSkinSelect item in _skinReset)
        {
            PlayerPrefs.SetInt(item.PlayerSpawn.name, 0);
        }
    }

    public void CancelReset()
    {
        _resetPanel.SetActive(false);
    }
}
