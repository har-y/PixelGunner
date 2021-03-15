using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverScreen : MonoBehaviour
{
    private Camera _camera;
    private Vector2 _screenPosition;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ScreenBounds();
    }

    private void ScreenBounds()
    {
        _screenPosition = _camera.WorldToScreenPoint(transform.position);

        if (_screenPosition.x > Screen.width || _screenPosition.x < 0 || _screenPosition.y > Screen.height || _screenPosition.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
