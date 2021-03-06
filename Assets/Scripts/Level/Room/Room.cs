using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room")]
    [SerializeField] private bool _firstRoom;
    [SerializeField] private bool _closeDoorsEnter;
    [SerializeField] private bool _activeRoom;
    private bool _roomClear;

    [Header("Room - Doors")]
    [SerializeField] private GameObject[] _doors;

    [Header("Map")]
    [SerializeField] private GameObject _roomHider;

    // Start is called before the first frame update
    void Start()
    {
        FirstRoom();
        OpenRoomDoor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);
            _roomHider.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateRoom();
            CloseRoomDoor();
        }
    }

    private void ActivateRoom()
    {
        _activeRoom = !_activeRoom;
    }

    private void CloseRoomDoor()
    {
        if (_closeDoorsEnter)
        {
            foreach (GameObject item in _doors)
            {
                item.SetActive(true);
            }
        }
    }

    public void OpenRoomDoor()
    {
        foreach (GameObject item in _doors)
        {
            item.SetActive(false);

            _closeDoorsEnter = false;
        }
    }

    public void FirstRoom()
    {
        if (_firstRoom)
        {
            _activeRoom = true;
            _roomHider.SetActive(false);
        }
    }

    public bool RoomActive
    {
        get
        {
            return _activeRoom;
        }
        set
        {
            _activeRoom = value;
        }
    }

    public bool CloseDoorEnter
    {
        get
        {
            return _closeDoorsEnter;
        }
        set
        {
            _closeDoorsEnter = value;
        }
    }

    public bool FirstRoomActive
    {
        set
        {
            _firstRoom = value;
        }
    }
}
