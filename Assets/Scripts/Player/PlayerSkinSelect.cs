using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinSelect : MonoBehaviour
{
    [Header("Player Skin Select")]
    [SerializeField] private GameObject _skinMessageText;
    [SerializeField] private PlayerController _playerSpawn;
    [SerializeField] private bool _toLock;
    private Vector3 _position;
    private bool _canSelect;

    // Start is called before the first frame update
    void Start()
    {
        if (_toLock)
        {
            if (PlayerPrefs.HasKey(_playerSpawn.name))
            {
                if (PlayerPrefs.GetInt(_playerSpawn.name) == 1)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_canSelect)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SkinSwitch();
            }
        }
    }

    private void SkinSwitch()
    {
        _position = PlayerController.instance.transform.position;

        Destroy(PlayerController.instance.gameObject);

        PlayerController newPlayer = Instantiate(_playerSpawn, _position, _playerSpawn.transform.rotation);
        PlayerController.instance = newPlayer;

        gameObject.SetActive(false);

        CameraController.instance.CameraTarget = newPlayer.transform;

        PlayerSkinManager.instance.ActivePlayer = newPlayer;
        PlayerSkinManager.instance.ActiveCharacterSelect.gameObject.SetActive(true);
        PlayerSkinManager.instance.ActiveCharacterSelect = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canSelect = true;
            _skinMessageText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canSelect = false;
            _skinMessageText.SetActive(false);
        }
    }

    public PlayerController PlayerSpawn
    {
        get
        {
            return _playerSpawn;
        }
        set
        {
            _playerSpawn = value;
        }
    }
}
