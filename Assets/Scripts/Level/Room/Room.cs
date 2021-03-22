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
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
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

    private void FirstRoom()
    {
        if (_firstRoom)
        {
            _activeRoom = true;
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
