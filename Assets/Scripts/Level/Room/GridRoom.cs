using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRoom : MonoBehaviour
{
    [Header("Room")]
    [SerializeField] private bool _firstRoom;
    [SerializeField] private bool _enterCloseDoors;
    [SerializeField] private GameObject[] _doors;


    // Start is called before the first frame update
    void Start()
    {
        FirstRoom();
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
            CloseRoomDoor();
        }
    }

    private void CloseRoomDoor()
    {
        if (_enterCloseDoors)
        {
            foreach (GameObject item in _doors)
            {
                item.SetActive(true);
            }
        }
    }

    private void FirstRoom()
    {
        if (_firstRoom)
        {
            CloseRoomDoor();
        }
    }
}
