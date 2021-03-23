using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    [Header("Room Center")]
    [SerializeField] private List<GameObject> _enemy = new List<GameObject>();
    [SerializeField] private bool _openDoors;
    [SerializeField] private Room _theRoom;

    // Start is called before the first frame update
    void Start()
    {
        RoomNotEmpty();
    }

    private void RoomNotEmpty()
    {
        if (_openDoors)
        {
            _theRoom.CloseDoorEnter = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RoomWithEnemy();
    }

    private void RoomWithEnemy()
    {
        if (_theRoom.RoomActive && _openDoors && _enemy.Count > 0)
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
                _theRoom.OpenRoomDoor();
            }
        }
    }

    public Room TheRoom
    {
        get
        {
            return _theRoom;
        }

        set
        {
            _theRoom = value;
        }
    }
}
