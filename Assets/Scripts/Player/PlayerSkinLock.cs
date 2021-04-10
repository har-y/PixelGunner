using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinLock : MonoBehaviour
{
    [Header("Player Skin Lock")]
    [SerializeField] private PlayerSkinSelect[] _skinsSelect;
    [SerializeField] private GameObject _skinLockMessageText;
    [SerializeField] private SpriteRenderer _lockSpriteRenderer;
    private PlayerSkinSelect _skinUnlock;

    private bool _canUnlock;

    // Start is called before the first frame update
    void Start()
    {
        _skinUnlock = _skinsSelect[Random.Range(0, _skinsSelect.Length)];
        _lockSpriteRenderer.sprite = _skinUnlock.PlayerSpawn.PlayerBodySprite.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canUnlock)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(_skinUnlock, transform.position, transform.rotation);

                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _skinLockMessageText.SetActive(true);
            _canUnlock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _skinLockMessageText.SetActive(false);
            _canUnlock = false;
        }
    }
}
