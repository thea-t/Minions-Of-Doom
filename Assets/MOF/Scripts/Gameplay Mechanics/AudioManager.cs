using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
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
