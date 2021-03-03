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

    [Header("Player Weapon")]
    [SerializeField] private Transform _weapon;
    private Vector3 _mousePosition;
    private Vector3 _screenPoint;
    private Vector2 _offset;
    private float _angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        _mousePosition = Input.mousePosition;
        _screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        _offset = new Vector2(_mousePosition.x - _screenPoint.x, _mousePosition.y - _screenPoint.y);
        _angle = Mathf.Atan2(_offset.y, _offset.x) * Mathf.Rad2Deg;
        _weapon.rotation = Quaternion.Euler(0f, 0f, _angle);
    }

    private void PlayerMove()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        _rigidbody2D.velocity = _moveInput * _moveSpeed;
    }
}
