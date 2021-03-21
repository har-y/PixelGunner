using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomPrefab
{
    [SerializeField] private bool _up;
    [SerializeField] private bool _down;
    [SerializeField] private bool _right;
    [SerializeField] private bool _left;

    public GameObject _prefab;

    public bool Up
    {
        get
        {
            return _up;
        }
    }
    public bool Down
    {
        get
        {
            return _down;
        }
    }
    public bool Left
    {
        get
        {
            return _left;
        }
    }
    public bool Right
    {
        get
        {
            return _right;
        }
    }

    public GameObject OutlinePrefab
    {
        get
        {
            return _prefab;
        }
    }
}
