using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandleRoomSpawner : MonoBehaviour
{
    void Start() {
        StartCoroutine(SceneLoader.FadeToScene("CandleRoom"));
    }
}
