using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Level Generator")]
    [SerializeField] private GameObject _layoutRoom;
    [SerializeField] private Transform _generatorPoint;
    [SerializeField] private LayerMask _roomLayer;
    [SerializeField] private Direction _direction;

    [Header("Level Generator - Room")]
    [SerializeField] private int _roomValue;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private float _xOffset = 18f;
    [SerializeField] private float _yOffset = 10f;

    [Header("Level Generator - Rooms")]
    private GameObject _endRoom;
    private List<GameObject> _roomObject;
    private GameObject _roomSlot;

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
        _roomObject = new List<GameObject>();
        _roomSlot = GameObject.FindGameObjectWithTag("Misc");

        InstantiateRoom();
        RoomGenerator();
    }

    private void RoomGenerator()
    {
        for (int i = 0; i < _roomValue; i++)
        {
            InstantiateRoom(i);

            while (Physics2D.OverlapCircle(_generatorPoint.position, 0.2f, _roomLayer))
            {
                PointMoveGenerator();
            }
        }
    }

    private void InstantiateRoom()
    {
        GameObject newRoom = Instantiate(_layoutRoom, _generatorPoint.position, _generatorPoint.rotation);
        newRoom.GetComponent<SpriteRenderer>().color = _startColor;
        newRoom.transform.parent = _roomSlot.transform;

        RandomDirection();
    }

    private void InstantiateRoom(int value)
    {
        GameObject newRoom = Instantiate(_layoutRoom, _generatorPoint.position, _generatorPoint.rotation);

        _roomObject.Add(newRoom);

        if (value + 1 == _roomValue)
        {
            newRoom.GetComponent<SpriteRenderer>().color = _endColor;
            _endRoom = newRoom;
            _roomObject.RemoveAt(_roomObject.Count - 1);
        }

        newRoom.transform.parent = _roomSlot.transform;

        RandomDirection();
    }

    private void RandomDirection()
    {
        _direction = (Direction)Random.Range(0, 4);
        PointMoveGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointMoveGenerator()
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
