using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room")]
    [SerializeField] private bool _firstRoom;
    [SerializeField] private bool _closeDoorsEnter;
    [SerializeField] private bool _openDoorsEnemy;
    private bool _activeRoom;
    private bool _roomClear;

    [Header("Room - Doors")]
    [SerializeField] private GameObject[] _doors;

    [Header("Room - Enemy")]
    [SerializeField] private List<GameObject> _enemy = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        FirstRoom();
    }

    // Update is called once per frame
    void Update()
    {
        RoomClear();
    }

    private void RoomClear()
    {
        if (_activeRoom && _openDoorsEnemy && _enemy.Count > 0)
        {
            for (int i = 0; i < _enemy.Count; i++)
            {
                if (_enemy[i] == null)
                {
                    _enemy.RemoveAt(i);

                    i--;
                }
            }

            if (_enemy.Count == 0)
            {
                _roomClear = true;
            }

            OpenRoomDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);

            _activeRoom = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CloseRoomDoor();

            _activeRoom = true;
        }
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

    private void OpenRoomDoor()
    {
        if (_roomClear)
        {
            foreach (GameObject item in _doors)
            {
                item.SetActive(false);

                _closeDoorsEnter = false;
            }
        }
    }

    private void FirstRoom()
    {
        if (_firstRoom)
        {
            CloseRoomDoor();

            _activeRoom = true;
        }
    }
}
