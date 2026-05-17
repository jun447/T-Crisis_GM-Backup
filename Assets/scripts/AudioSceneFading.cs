using UnityEngine;
using System;

public class AudioSceneFading : MonoBehaviour
{
    public static AudioSceneFading Instance { get; private set; }

    public static float MusicVolume = 0.5f;
    public static float EffectsVolume = 0.5f;

    
    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;

    private AudioSource musicAudioSource;
    private AudioSource sfxAudioSource;

    public static int MusicTotal = 2;
    public static int MusicCurrentlyPlaying = -1;
    public static int EffectsTotal = 2;

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
    public static long LastTicks = DateTime.Now.Ticks;

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
        musicAudioSource.volume = MusicVolume;
        sfxAudioSource.volume = EffectsVolume;
    }

    void Update()
    {
        if (musicAudioSource != null) musicAudioSource.volume = MusicVolume;
        if (sfxAudioSource != null) sfxAudioSource.volume = EffectsVolume;
    }

    public static void PlayMusic(int musicToPlay, int loop)
    {
        if (Instance == null) return;
        if (musicToPlay < 0 || musicToPlay >= Instance.musicClips.Length) return;

        MusicCurrentlyPlaying = musicToPlay;
        Instance.musicAudioSource.clip = Instance.musicClips[musicToPlay];
        Instance.musicAudioSource.loop = (loop == -1);
        Instance.musicAudioSource.volume = MusicVolume;
        Instance.musicAudioSource.Play();
    }

    public static void PlaySoundEffect(int soundEffectToPlay, int loop)
    {
        if (Instance == null) return;
        if (soundEffectToPlay < 0 || soundEffectToPlay >= Instance.sfxClips.Length) return;

        Instance.sfxAudioSource.volume = EffectsVolume;

        if (loop == -1)
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
