using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Health Pickup")]
    [SerializeField] private int _healthValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        PlayerHealthController.instance.PlayerHeal(_healthValue);
        Destroy(gameObject);
    }
}
