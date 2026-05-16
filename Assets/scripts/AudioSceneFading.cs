using UnityEngine;
using System;

public class AudioSceneFading : MonoBehaviour
{
    public static AudioSceneFading Instance { get; private set; }

    public static float MusicVolume = 0.5f;
    public static float EffectsVolume = 0.5f;

    //int MusicPlayer
    public static int MusicTotal = 2;
    public static int MusicCurrentlyPlaying = -1;

    //int EffectPlayer = []
    public static int EffectsTotal = 2;

    public const int FadeIn            = 0;
    public const int FadeOut           = 1;
    public const int FadeNone          = -1;
    public static int ScreenFadeStatus = FadeIn;

    public static float ScreenFadeAlpha = 1.0f;

    public const int SceneClickToContinue      = 0;
    public const int SceneUnity                = 1;
    public const int SceneTitle                = 2;
    public static int CurrentSceneToDisplay = SceneClickToContinue;
    public static int NextSceneToDisplay = SceneUnity;

    public static float ScreenDisplayTimer = 0.0f;

    public static long LastTicks = DateTime.Now.Ticks;

    void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
    }

    void Update()
    {
        
    }

    void OnGui()
    {
        // common GUI code goes here
    }

    public static void PlayMusic(int musicToPlay, int loop) // loop of: "-1" means loop forever
    {

    }

    public static void PlaySoundEffect(int soundEffectToPlay, int loop)
    {

    }
}
