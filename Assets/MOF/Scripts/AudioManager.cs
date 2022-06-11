using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Minion Sounds")]
    public AudioClip maleMinionOnDropped;
    public AudioClip femaleMinionOnDropped;

    public AudioClip maleMinionOnGrabbed;
    public AudioClip femaleMinionOnGrabbed;

    public AudioClip maleMinionOnUnsuccessfulGrab;
    public AudioClip femaleMinionOnUnsuccessfulGrab;

    public AudioClip maleMinionOnTableCollided;
    public AudioClip femaleMinionOnTableCollided;

    [Header("Win Sounds")]
    public AudioClip wonAgainstBasicEnemy;
    public AudioClip wonAgainstEliteEnemy;
    public AudioClip wonAgainstBossEnemy;
}
