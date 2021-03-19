using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Level Generator")]
    [SerializeField] private GameObject _layoutRoom;
    [SerializeField] private Transform _generatorPoint;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;

    [Header("Level Generator - Room")]
    [SerializeField] private Direction _direction;
    [SerializeField] private float _xOffset = 18f;
    [SerializeField] private float _yOffset = 10f;
    [SerializeField] private int _roomValue;

    private enum Direction
    {
        up,
        right,
        down,
        left
    }

    // Start is called before the first frame update
    void Start()
    {
        StartRoomGenerator();

        for (int i = 0; i < _roomValue; i++)
        {
            RoomGenerator();
        }
    }

    private void StartRoomGenerator()
    {
        Instantiate(_layoutRoom, _generatorPoint.position, _generatorPoint.rotation).GetComponent<SpriteRenderer>().color = _startColor;
        RandomRandomDirection();
    }

    private void RoomGenerator()
    {
        Instantiate(_layoutRoom, _generatorPoint.position, _generatorPoint.rotation);
        RandomRandomDirection();
    }

    private void RandomRandomDirection()
    {
        _direction = (Direction)Random.Range(0, 4);
        GeneratorPointMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GeneratorPointMove()
    {
        switch (_direction)
        {
            case Direction.up:
                _generatorPoint.position += new Vector3(0f, _yOffset, 0f);
                break;
            case Direction.right:
                _generatorPoint.position += new Vector3(_xOffset, 0f, 0f);
                break;
            case Direction.down:
                _generatorPoint.position += new Vector3(0f, -_yOffset, 0f);
                break;
            case Direction.left:
                _generatorPoint.position += new Vector3(-_xOffset, 0f, 0f);
                break;
            default:
                break;
        }
    }
}
