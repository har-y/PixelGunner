using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomPrefab
{
    [SerializeField] private GameObject _singleUp;
    [SerializeField] private GameObject _singleDown;
    [SerializeField] private GameObject _singleLeft;
    [SerializeField] private GameObject _singleRight;

    [SerializeField] private GameObject _doubleUpDown;
    [SerializeField] private GameObject _doubleLeftRight;
    [SerializeField] private GameObject _doubleUpLeft;
    [SerializeField] private GameObject _doubleUpRight;
    [SerializeField] private GameObject _doubleDownLeft;
    [SerializeField] private GameObject _doubleDownRight;

    [SerializeField] private GameObject _tripleUpLeftRight;
    [SerializeField] private GameObject _tripleDownLeftRight;
    [SerializeField] private GameObject _tripleUpDownLeft;
    [SerializeField] private GameObject _tripleUpDownRight;

    [SerializeField] private GameObject _quadrupleUpDownLeftRight;
}
