using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton class that holds all audio references and plays scene musics.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    
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
    
    [Header("Musics")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip candleMusic;
    public AudioClip mapMusic;
    
    public static AudioManager Instance { get; private set; }

    /// <summary>
    /// Initializes the audio manager and stops it from getting destroyed when a new scene loads.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    
    /// <summary>
    /// This is called when a new scene is loaded. It plays different music clips based on which scene is opened.
    /// </summary>
    private void ChangedActiveScene(Scene current, Scene next)
    {
        switch (next.name)
        {
            case "Menu":
            {
                musicSource.clip = menuMusic;
                musicSource.Play();
                break;
            }
            case "Intro":
            {
                musicSource.clip = menuMusic;
                musicSource.Play();
                break;
            }
            case "Map":
            {
                musicSource.clip = mapMusic;
                musicSource.Play();
                break;
            }
            case "GameScene":
            {
                musicSource.clip = gameMusic;
                musicSource.Play();
                break;
            }
            case "CandleRoom":
            {
                musicSource.clip = candleMusic;
                musicSource.Play();
                break;
            }
        }
    }
}
