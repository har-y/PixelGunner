using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Camera")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _target;

    [Header("Map")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _mapCamera;
    [SerializeField] private GameObject _mapMarker;
    private bool _mapActive;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        MapDeactivate();
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        CameraMap();
    }

    private void CameraMap()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!_mapActive)
            {
                MapActivate();
            }
            else
            {
                MapDeactivate();
            }
        }
    }

    private void CameraMove()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.position.x, _target.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
        }
    }

    public void ChangeTarget(Transform target)
    {
        _target = target;
    }

    public void MapActivate()
    {
        if (!LevelManager.instance.IsPause)
        {
            _mapActive = true;

            _mapCamera.enabled = true;
            _mainCamera.enabled = false;

            _mapMarker.SetActive(true);

            PlayerController.instance.CanMove = false;
            Time.timeScale = 0f;
            UIController.instance.Map.SetActive(false);
        }
    }

    public void MapDeactivate()
    {
        if (!LevelManager.instance.IsPause)
        {
            _mapActive = false;

            _mapCamera.enabled = false;
            _mainCamera.enabled = true;

            _mapMarker.SetActive(false);

            PlayerController.instance.CanMove = true;
            Time.timeScale = 1f;
            UIController.instance.Map.SetActive(true);
        }
    }
}
