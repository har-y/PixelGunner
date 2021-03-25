using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [Header("Breakable")]
    [SerializeField] private GameObject[] _brokenPieces;
    private GameObject[] _brokenPiece;
    private GameObject _pieceSlot;
    [SerializeField] private int _boxSound;


    [Header("Breakable - Drop")]
    [SerializeField] private bool _drop;
    [SerializeField] GameObject[] _dropPrefab;
    [SerializeField] private float _dropChance;
    private float _dropValue;
    private int _dropItem;
    private GameObject _dropSlot;

    // Start is called before the first frame update
    void Start()
    {
        _pieceSlot = GameObject.FindGameObjectWithTag("Misc");
        _dropSlot = GameObject.FindGameObjectWithTag("Misc");

        _dropValue = Random.Range(0f, 100f);
        _dropItem = Random.Range(0, _dropPrefab.Length);

        _brokenPiece = new GameObject[_brokenPieces.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerController.instance.DashCounter > 0)
            {
                BoxDestroy();
            }
        }
        else if (other.CompareTag("Player Bullet"))
        {
            BoxDestroy();
        }
    }

    private void BoxDestroy()
    {
        Destroy(gameObject);

        AudioManager.instance.PlaySoundClip(_boxSound);

        InstantiatePieces();
        InstantiateDrop();
    }

    private void InstantiatePieces()
    {
        for (int i = 0; i < _brokenPieces.Length; i++)
        {
            GameObject piece = Instantiate(_brokenPieces[i], transform.position, transform.rotation);
            _brokenPiece[i] = piece;
            _brokenPiece[i].transform.parent = _pieceSlot.transform;
        }
    }

    private void InstantiateDrop()
    {
        if (_drop)
        {
            if (_dropValue <= _dropChance)
            {
                GameObject drop = Instantiate(_dropPrefab[_dropItem], transform.position, transform.rotation);
                drop.transform.parent = _dropSlot.transform;
            }
        }
    }
}
