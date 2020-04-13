using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOverlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int numUIOverlayObjects = FindObjectsOfType<Canvas>().Length;        //if more than music player in scene the ndestroy ourselves
        if (numUIOverlayObjects > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


}
