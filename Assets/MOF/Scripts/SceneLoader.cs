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

    private void Awake()
    {
        Application.targetFrameRate = 60;

        DontDestroyOnLoad(this);
    }

    public static IEnumerator FadeToScene(string sceneName) {
        yield return new WaitForSeconds(FADE_DURATION);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}