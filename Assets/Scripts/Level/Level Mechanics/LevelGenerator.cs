using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    [Header("Level Generator")]
    [SerializeField] private GameObject _layoutRoom;
    [SerializeField] private Transform _generatorPoint;
    [SerializeField] private LayerMask _roomLayer;
    [SerializeField] private Direction _direction;

    [Header("Level Generator - Rooms")]
    [SerializeField] private int _roomValue;
    [SerializeField] private bool _shop;
    [SerializeField] private bool _chest;
    [SerializeField] private int _minShopDistance;
    [SerializeField] private int _maxShopDistance;
    [SerializeField] private int _minChestDistance;
    [SerializeField] private int _maxChestDistance;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private Color _shopColor;
    [SerializeField] private Color _chestColor;
    [SerializeField] private float _xOffset = 18f;
    [SerializeField] private float _yOffset = 10f;

    [Header("Level Generator - Rooms Center")]
    [SerializeField] private RoomCenter _centerStart;
    [SerializeField] private RoomCenter _centerEnd;
    [SerializeField] private RoomCenter _centerShop;
    [SerializeField] private RoomCenter _centerChest;
    [SerializeField] private RoomCenter[] _center;

    [Header("Level Generator - Room")]
    private GameObject _roomSlot;
    private GameObject _endRoom;
    private GameObject _shopRoom;
    private GameObject _chestRoom;
    private List<GameObject> _roomObject = new List<GameObject>();
    [SerializeField] private List<RoomPrefab> _roomPrefab;
    [SerializeField] private List<GameObject> _generateOutline = new List<GameObject>();


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
        RoomsGenerator();
    }

    private void InstantiateRoom()
    {
        GameObject newRoom = Instantiate(_layoutRoom, _generatorPoint.position, _generatorPoint.rotation);
        newRoom.GetComponent<SpriteRenderer>().color = _startColor;
        newRoom.transform.parent = _roomSlot.transform;

        RandomDirection();
    }

    private void RoomsGenerator()
    {
        for (int i = 0; i < _roomValue; i++)
        {
            GameObject newRoom = Instantiate(_layoutRoom, _generatorPoint.position, _generatorPoint.rotation);
            newRoom.transform.parent = _roomSlot.transform;

            _roomObject.Add(newRoom);

            if (i + 1 == _roomValue)
            {
                newRoom.GetComponent<SpriteRenderer>().color = _endColor;
                _endRoom = newRoom;
                _roomObject.RemoveAt(_roomObject.Count - 1);
            }

            RandomDirection();

            while (Physics2D.OverlapCircle(_generatorPoint.position, 0.2f, _roomLayer))
            {
                PointMoveGenerator();
            }
        }

        ShopGenerator();
        ChestGenerator();

        RoomOutlineGenerator();
    }

    private void ShopGenerator()
    {
        if (_shop)
        {
            int shopSelect = Random.Range(_minShopDistance, _maxShopDistance + 1);
            _shopRoom = _roomObject[shopSelect];
            _roomObject.RemoveAt(shopSelect);
            _shopRoom.GetComponent<SpriteRenderer>().color = _shopColor;
        }
    }

    private void ChestGenerator()
    {
        if (_chest)
        {
            int chestSelect = Random.Range(_minChestDistance, _maxChestDistance + 1);
            _chestRoom = _roomObject[chestSelect];
            _roomObject.RemoveAt(chestSelect);
            _chestRoom.GetComponent<SpriteRenderer>().color = _chestColor;
        }
    }

    private void RoomOutlineGenerator()
    {
        InstantiateRoomOutline(Vector3.zero);

        foreach (GameObject room in _roomObject)
        {
            InstantiateRoomOutline(room.transform.position);
        }

        if (_shop)
        {
            InstantiateRoomOutline(_shopRoom.transform.position);
        }

        if (_chest)
        {
            InstantiateRoomOutline(_chestRoom.transform.position);
        }

        InstantiateRoomOutline(_endRoom.transform.position);

        foreach (GameObject outline in _generateOutline)
        {
            bool generateCenter = true;

            if (outline.transform.position == Vector3.zero)
            {
                RoomCenter centerStartOutline = Instantiate(_centerStart, outline.transform.position, transform.rotation);
                centerStartOutline.TheRoom = outline.GetComponent<Room>();
                centerStartOutline.TheRoom.FirstRoomActive = true;
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

            if (_shop)
            {
                if (outline.transform.position == _shopRoom.transform.position)
                {
                    RoomCenter centerShopOutline = Instantiate(_centerShop, outline.transform.position, transform.rotation);
                    centerShopOutline.TheRoom = outline.GetComponent<Room>();
                    centerShopOutline.transform.parent = _roomSlot.transform;
                    generateCenter = false;
                }
            }

            if (_chest)
            {
                if (outline.transform.position == _chestRoom.transform.position)
                {
                    RoomCenter centerChestOutline = Instantiate(_centerChest, outline.transform.position, transform.rotation);
                    centerChestOutline.TheRoom = outline.GetComponent<Room>();
                    centerChestOutline.transform.parent = _roomSlot.transform;
                    generateCenter = false;
                }
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


    private void RandomDirection()
    {
        _direction = (Direction)Random.Range(0, 4);
        PointMoveGenerator();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif
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
