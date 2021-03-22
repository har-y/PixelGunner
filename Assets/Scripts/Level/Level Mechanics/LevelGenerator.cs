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
    private List<GameObject> _roomObject = new List<GameObject>();
    [SerializeField] private List<RoomPrefab> _roomPrefab;
    [SerializeField] private List<GameObject> _generateOutline = new List<GameObject>();
    private GameObject _roomSlot;

    [Header("Level Generator - Rooms Center")]
    [SerializeField] private RoomCenter _centerStart;
    [SerializeField] private RoomCenter _centerEnd;
    [SerializeField] private RoomCenter[] _center;

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

        RoomOutlineGenerator();
    }

    private void RoomOutlineGenerator()
    {
        InstantiateRoomOutline(Vector3.zero);

        foreach (GameObject room in _roomObject)
        {
            InstantiateRoomOutline(room.transform.position);
        }

        InstantiateRoomOutline(_endRoom.transform.position);

        foreach (GameObject outline in _generateOutline)
        {
            bool generateCenter = true;

            if (outline.transform.position == Vector3.zero)
            {
                RoomCenter centerStartOutline = Instantiate(_centerStart, outline.transform.position, transform.rotation);
                centerStartOutline.TheRoom = outline.GetComponent<Room>();
                centerStartOutline.transform.parent = _roomSlot.transform;
                generateCenter = false;
            }

            if (outline.transform.position == _endRoom.transform.position)
            {
                RoomCenter centerEndOutline = Instantiate(_centerEnd, outline.transform.position, transform.rotation);
                centerEndOutline.TheRoom = outline.GetComponent<Room>();
                centerEndOutline.transform.parent = _roomSlot.transform;
                generateCenter = false;
            }

            if (generateCenter)
            {
                int centerSelect = Random.Range(0, _center.Length);
                RoomCenter centerOutline = Instantiate(_center[centerSelect], outline.transform.position, transform.rotation);
                centerOutline.TheRoom = outline.GetComponent<Room>();
                centerOutline.transform.parent = _roomSlot.transform;
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

    private void InstantiateRoom(int loopValue)
    {
        GameObject newRoom = Instantiate(_layoutRoom, _generatorPoint.position, _generatorPoint.rotation);
        newRoom.transform.parent = _roomSlot.transform;

        _roomObject.Add(newRoom);

        if (loopValue + 1 == _roomValue)
        {
            newRoom.GetComponent<SpriteRenderer>().color = _endColor;
            _endRoom = newRoom;
            _roomObject.RemoveAt(_roomObject.Count - 1);
        }

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

    private void PointMoveGenerator()
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

    private RoomPrefab PickRoom(bool up, bool down, bool right, bool left)
    {
        foreach (RoomPrefab rp in _roomPrefab)
        {
            if (rp.Up == up && rp.Down == down && rp.Right == right && rp.Left == left)
            {
                return rp;
            }
        }
        return null;
    }

    private void InstantiateRoomOutline(Vector3 position)
    {
        bool up = Physics2D.OverlapCircle(position + new Vector3(0f, _yOffset, 0f), 0.2f, _roomLayer);
        bool down = Physics2D.OverlapCircle(position + new Vector3(0f, -_yOffset, 0f), 0.2f, _roomLayer);
        bool right = Physics2D.OverlapCircle(position + new Vector3(_xOffset, 0f, 0f), 0.2f, _roomLayer);
        bool left = Physics2D.OverlapCircle(position + new Vector3(-_xOffset, 0f, 0f), 0.2f, _roomLayer);

        RoomPrefab roomPrefab = PickRoom(up, down, right, left);

        if (roomPrefab != null)
        {
            GameObject room = Instantiate(roomPrefab.OutlinePrefab, position, transform.rotation, transform);
            room.transform.parent = _roomSlot.transform;
            _generateOutline.Add(room);
        }
    }
}
