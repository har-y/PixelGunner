using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Level Manager")]
    [SerializeField] private float _waitTime;
    [SerializeField] private string _nextLevel;

    [Header("Pause")]
    [SerializeField] private bool _isPause;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public IEnumerator LevelEndCoroutine()
    {
        AudioManager.instance.PlayMusic(7);

        PlayerController.instance.CanMove = false;

        UIController.instance.FadeOn();

        yield return new WaitForSeconds(_waitTime);

        SceneManager.LoadScene(_nextLevel);
    }

    public void Pause()
    {
        _isPause = !_isPause;

        if (_isPause)
        {
            UIController.instance.PauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public bool IsPause
    {
        get
        {
            return _isPause;
        }
    }
}
