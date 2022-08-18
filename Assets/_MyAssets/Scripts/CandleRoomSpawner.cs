using UnityEngine;

/// <summary>
/// Simple class which loads candle room scene on start.
/// </summary>
public class CandleRoomSpawner : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SceneLoader.FadeToScene("CandleRoom"));
    }
}