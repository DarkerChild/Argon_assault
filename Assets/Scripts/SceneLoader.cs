using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] float LevelLoadDelayTime = 2f;

    private void Update()
    {
        CheckSpacePressed();

        int score = 99;
        int desiredLength = 9;
        String yourString = "00000000" + score.ToString();
        String yourFormattedString = yourString.Substring(yourString.Length - desiredLength, yourString.Length);
    }

    private void CheckSpacePressed()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            StartCoroutine(LoadNextLevel(LevelLoadDelayTime));
        }
    }

    IEnumerator LoadNextLevel(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(1);
        }
    }
}

