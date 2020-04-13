//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float LevelLoadDelayTiome = 2f;

    void Start()
    {
        Invoke("LoadNextLevel", LevelLoadDelayTiome);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
