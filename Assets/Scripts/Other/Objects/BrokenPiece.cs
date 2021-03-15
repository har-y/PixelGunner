using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPiece : MonoBehaviour
{
    [Header("Broken Piece")]
    private SpriteRenderer _piece;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _decelerationSpeed;
    private Vector3 _moveDirection;

    [Header("Broken Piece - Time")]
    [SerializeField] private float _pieceTime;
    [SerializeField] private float _fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        _piece = GetComponent<SpriteRenderer>();

        _moveDirection.x = Random.Range(-_moveSpeed, _moveSpeed);
        _moveDirection.y = Random.Range(-_moveSpeed, _moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        PieceMove();
        PieceDestroy();
    }


    private void PieceMove()
    {
        transform.position += _moveDirection * Time.deltaTime;
        _moveDirection = Vector3.Lerp(_moveDirection, Vector3.zero, _decelerationSpeed * Time.deltaTime);
    }

    private void PieceDestroy()
    {
        _pieceTime -= Time.deltaTime;

        if (_pieceTime <= 0)
        {
            _piece.color = new Color(_piece.color.r, _piece.color.g, _piece.color.b, Mathf.MoveTowards(_piece.color.a, 0f, _fadeTime * Time.deltaTime));

            Destroy(gameObject, 2f);
        }
    }
}
