using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [Header("Breakable")]
    [SerializeField] private GameObject[] _brokenPieces;
    private GameObject[] _brokenPiece;
    private GameObject _pieceSlot;

    // Start is called before the first frame update
    void Start()
    {
        _pieceSlot = GameObject.FindGameObjectWithTag("Misc");
        _brokenPiece = new GameObject[_brokenPieces.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerController.instance.DashCounter > 0)
            {
                Destroy(gameObject);

                for (int i = 0; i < _brokenPieces.Length; i++)
                {
                    GameObject piece = Instantiate(_brokenPieces[i], transform.position, transform.rotation);
                    _brokenPiece[i] = piece;
                    _brokenPiece[i].transform.parent = _pieceSlot.transform;
                }
            }
        }
    }
}
