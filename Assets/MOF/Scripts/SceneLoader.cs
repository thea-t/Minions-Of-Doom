using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private static Image blackImage;
    private const float FADE_DURATION = 5.5f;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        DontDestroyOnLoad(this);
        blackImage = GetComponentInChildren<Image>();
    }

    public static void FadeToScene(string sceneName)
    {
        blackImage.DOFade(1f, FADE_DURATION).onComplete = () =>
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            blackImage.DOFade(0f, FADE_DURATION);
        };
    }
}