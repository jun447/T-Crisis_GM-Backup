using UnityEngine;

public class AudioSceneFading : MonoBehaviour
{
    public static AudioSceneFading Instance { get; private set; }

    public static float MusicVolume = 0.5f;
    public static float EffectsVolume = 0.5f;

    
    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;

    private AudioSource musicAudioSource;
    private AudioSource sfxAudioSource;

    public static int MusicCurrentlyPlaying = -1;
    private const int LoopForever = -1;

    public const int FadeIn            = 0;
    public const int FadeOut           = 1;
    public const int FadeNone          = -1;
    public static int ScreenFadeStatus = FadeIn;

    public static float ScreenFadeAlpha = 1.0f;

    public const int SceneClickToContinue     = 0;
    public const int SceneUnity               = 1;
    public const int SceneTitle               = 2;
    public static int CurrentSceneToDisplay = SceneClickToContinue;
    public static int NextSceneToDisplay = SceneUnity;

    public static float ScreenDisplayTimer = 0.0f;

    private float lastMusicVolume;
    private float lastEffectsVolume;

    void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
        musicAudioSource = gameObject.AddComponent<AudioSource>();
        sfxAudioSource = gameObject.AddComponent<AudioSource>();
        if (musicAudioSource == null || sfxAudioSource == null)
        {
            Debug.LogError("AudioSceneFading: Failed to create AudioSource components.");
            enabled = false;
            return;
        }
        musicAudioSource.volume = MusicVolume;
        sfxAudioSource.volume = EffectsVolume;
        lastMusicVolume = MusicVolume;
        lastEffectsVolume = EffectsVolume;
    }

    void Update()
    {
        if (musicAudioSource != null && !Mathf.Approximately(lastMusicVolume, MusicVolume))
        {
            musicAudioSource.volume = MusicVolume;
            lastMusicVolume = MusicVolume;
        }

        if (sfxAudioSource != null && !Mathf.Approximately(lastEffectsVolume, EffectsVolume))
        {
            sfxAudioSource.volume = EffectsVolume;
            lastEffectsVolume = EffectsVolume;
        }
    }

    public static void PlayMusic(int musicToPlay, int loop)
    {
        if (Instance == null) return;
        if (Instance.musicAudioSource == null) return;
        if (Instance.musicClips == null || Instance.musicClips.Length == 0) return;
        if (musicToPlay < 0 || musicToPlay >= Instance.musicClips.Length) return;
        if (Instance.musicClips[musicToPlay] == null) return;

        MusicCurrentlyPlaying = musicToPlay;
        Instance.musicAudioSource.clip = Instance.musicClips[musicToPlay];
        Instance.musicAudioSource.loop = (loop == LoopForever);
        Instance.musicAudioSource.volume = MusicVolume;
        Instance.musicAudioSource.Play();
    }

    public static void PlaySoundEffect(int soundEffectToPlay, int loop)
    {
        if (Instance == null) return;
        if (Instance.sfxAudioSource == null) return;
        if (Instance.sfxClips == null || Instance.sfxClips.Length == 0) return;
        if (soundEffectToPlay < 0 || soundEffectToPlay >= Instance.sfxClips.Length) return;
        if (Instance.sfxClips[soundEffectToPlay] == null) return;

        Instance.sfxAudioSource.volume = EffectsVolume;

        if (loop == LoopForever)
        {
            Instance.sfxAudioSource.clip = Instance.sfxClips[soundEffectToPlay];
            Instance.sfxAudioSource.loop = true;
            Instance.sfxAudioSource.Play();
        }
        else
        {
            Instance.sfxAudioSource.PlayOneShot(Instance.sfxClips[soundEffectToPlay], EffectsVolume);
        }
    }
}
