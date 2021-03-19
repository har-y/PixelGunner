using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Level Generator")]
    [SerializeField] private GameObject _layoutRoom;
    [SerializeField] private int _layoutSpace;

    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;

    [SerializeField] private Transform _generatorPoint;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_layoutRoom, _generatorPoint.position, _generatorPoint.rotation).GetComponent<SpriteRenderer>().color = _startColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
