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
    [Header("Big")]
    [SerializeField] private Camera _mapCamera;
    [SerializeField] private GameObject _mapMarker;
    private bool _bigMapActive;
    [Header("Mini")]
    private bool _miniMapActive;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        BigMapDeactivate();
        MiniMapDeactivate();
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
            if (!_bigMapActive)
            {
                BigMapActivate();
            }
            else
            {
                BigMapDeactivate();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!_miniMapActive)
            {
                MiniMapActivate();
            }
            else
            {
                MiniMapDeactivate();
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

    public void BigMapActivate()
    {
        if (!LevelManager.instance.IsPause)
        {
            _bigMapActive = true;

            _mapCamera.enabled = true;
            _mainCamera.enabled = false;

            _mapMarker.SetActive(true);

            PlayerController.instance.CanMove = false;
            Time.timeScale = 0f;
            UIController.instance.FullMap.SetActive(true);

            if (_miniMapActive)
            {
                UIController.instance.MiniMap.SetActive(false);
            }
        }
    }

    public void BigMapDeactivate()
    {
        if (!LevelManager.instance.IsPause)
        {
            _bigMapActive = false;

            _mapCamera.enabled = false;
            _mainCamera.enabled = true;

            _mapMarker.SetActive(true);

            PlayerController.instance.CanMove = true;
            Time.timeScale = 1f;
            UIController.instance.FullMap.SetActive(false);

            if (_miniMapActive)
            {
                UIController.instance.MiniMap.SetActive(true);
            }
        }
    }

    public void MiniMapActivate()
    {
        if (!LevelManager.instance.IsPause)
        {
            _miniMapActive = true;
            UIController.instance.MiniMap.SetActive(true);
        }
    }

    public void MiniMapDeactivate()
    {
        if (!LevelManager.instance.IsPause)
        {
            _miniMapActive = false;
            UIController.instance.MiniMap.SetActive(false);
        }
    }
}
