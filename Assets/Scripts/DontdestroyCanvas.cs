using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontdestroyCanvas : MonoBehaviour
{
    public static DontdestroyCanvas Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
