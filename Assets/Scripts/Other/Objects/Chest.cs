using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Chest")]
    [SerializeField] private SpriteRenderer _chestSpriteRenderer;
    [SerializeField] private Transform _chestSpawnPoint;
    [SerializeField] private GameObject _openText;
    [SerializeField] private Sprite _chestOpen;
    private bool _canOpen;
    private bool _isOpen;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private int _chestSound;

    [Header("Chest - Drop")]
    [SerializeField] private bool _drop;
    [SerializeField] GameObject[] _dropPrefab;
    [SerializeField] private float _dropChance;
    private float _dropValue;
    private int _dropItem;
    private GameObject _dropSlot;

    // Start is called before the first frame update
    void Start()
    {
        _dropSlot = GameObject.FindGameObjectWithTag("Misc");

        _dropValue = Random.Range(0f, 100f);
        _dropItem = Random.Range(0, _dropPrefab.Length);
    }

    // Update is called once per frame
    void Update()
    {
        ChestControl();
    }

    private void ChestControl()
    {
        if (_canOpen && !_isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChestOpen();
                KeepChestOpen();
            }
        }

        if (_isOpen)
        {
            ScaleDownChest();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_isOpen)
            {
                _openText.SetActive(true);
                _canOpen = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _openText.SetActive(false);
            _canOpen = false;
        }
    }

    private void ChestOpen()
    {
        AudioManager.instance.PlaySoundClip(_chestSound);

        InstantiateDrop();
    }

    private void InstantiateDrop()
    {
        if (_drop)
        {
            if (_dropValue <= _dropChance)
            {
                GameObject drop = Instantiate(_dropPrefab[_dropItem], _chestSpawnPoint.position, transform.rotation);
                drop.transform.parent = _dropSlot.transform;
            }
        }
    }

    private void KeepChestOpen()
    {
        _chestSpriteRenderer.sprite = _chestOpen;

        ScaleUpChest();

        _isOpen = true;
    }

    private void ScaleUpChest()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    private void ScaleDownChest()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * _scaleSpeed );
    }
}
