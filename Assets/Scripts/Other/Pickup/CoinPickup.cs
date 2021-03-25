using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [Header("Coin Pickup")]
    [SerializeField] private int _coinValue;
    [SerializeField] private bool _isCollectable;
    [SerializeField] private float _collectableTime;
    [SerializeField] private int _pickupSound;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CollectableCoroutine(_collectableTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isCollectable)
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        LevelManager.instance.GetCoins(_coinValue);
        AudioManager.instance.PlaySoundClip(_pickupSound);
        Destroy(gameObject);
    }

    IEnumerator CollectableCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        _isCollectable = true;
    }
}
