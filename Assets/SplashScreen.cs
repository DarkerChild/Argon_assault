using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] float LevelLoadDelayTiome = 2f;

    // Start is called before the first frame update
    void Start()
    {
        MoveToNextLevel();
        DontDestroyOnLoad(this.gameObject);
    }

    private void MoveToNextLevel()
    {
        Invoke("LoadNextLevel", LevelLoadDelayTiome);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
