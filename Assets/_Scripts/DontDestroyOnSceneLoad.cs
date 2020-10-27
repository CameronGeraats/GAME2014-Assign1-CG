using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnSceneLoad : MonoBehaviour
{
    static GameObject instance;
        
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }        
    }

}
