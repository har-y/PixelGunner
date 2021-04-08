using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinSelect : MonoBehaviour
{
    [Header("Player Skin Select")]
    [SerializeField] private GameObject _skinMessageText;
    [SerializeField] private PlayerController _playerSpawn;
    private Vector3 _position;
    private bool _canSelect;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
