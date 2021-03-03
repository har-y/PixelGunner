using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Player Movement")]
    [SerializeField] private float _moveSpeed;
    private Vector2 _moveInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        _rigidbody2D.velocity = _moveInput * _moveSpeed;
    }
}
