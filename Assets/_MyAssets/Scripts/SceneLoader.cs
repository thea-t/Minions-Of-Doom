using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private const float FADE_DURATION = 2f;

    /// <summary>
    /// Sets the target framerate of the game to 60.
    /// </summary>
    private void Awake()
    {
        Application.targetFrameRate = 60;

        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Load the given scene after a delay.
    /// </summary>
    public static IEnumerator FadeToScene(string sceneName)
    {
        yield return new WaitForSeconds(FADE_DURATION);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}