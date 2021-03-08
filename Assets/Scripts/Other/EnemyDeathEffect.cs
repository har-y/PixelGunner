using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    [Header("Death Effect")]
    [SerializeField] private SpriteRenderer _deathEffectShape;
    [SerializeField] private Sprite[] _deathEffects;
    private int _randomEffect;
    private int _randomRotation;

    // Start is called before the first frame update
    void Start()
    {
        _randomRotation = Random.Range(0, 4);
        _randomEffect = Random.Range(0, _deathEffects.Length);

        _deathEffectShape.sprite = _deathEffects[_randomEffect];

        transform.rotation = Quaternion.Euler(0f, 0f, _randomRotation * 90f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
