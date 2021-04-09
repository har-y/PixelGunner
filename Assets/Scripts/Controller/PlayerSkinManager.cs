using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    public static PlayerSkinManager instance;

    [Header("Player Skin Manager")]
    [SerializeField] private PlayerController _activePlayer;
    [SerializeField] private PlayerSkinSelect _activeCharacterSelect;

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
        
    }

    public PlayerController ActivePlayer
    {
        get
        {
            return _activePlayer;
        }
        set
        {
            _activePlayer = value;
        }
    }

    public PlayerSkinSelect ActiveCharacterSelect
    {
        get
        {
            return _activeCharacterSelect;
        }
        set
        {
            _activeCharacterSelect = value;
        }
    }
}
