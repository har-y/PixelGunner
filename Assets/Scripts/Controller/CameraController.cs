using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Camera")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _target;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
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
}
