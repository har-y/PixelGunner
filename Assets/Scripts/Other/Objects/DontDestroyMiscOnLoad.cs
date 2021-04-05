using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMiscOnLoad : MonoBehaviour
{
    public static DontDestroyMiscOnLoad instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyAllPrevious()
    {
        foreach (Transform childTransform in gameObject.transform)
        {
            Destroy(childTransform.gameObject);
        }
    }
}
