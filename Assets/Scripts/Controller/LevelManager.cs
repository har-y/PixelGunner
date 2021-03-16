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

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LevelEndCoroutine()
    {
        AudioManager.instance.PlayMusic(7);

        PlayerController.instance.CanMove = false;

        yield return new WaitForSeconds(_waitTime);

        SceneManager.LoadScene(_nextLevel);
    }
}
