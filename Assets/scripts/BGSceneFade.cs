using UnityEngine;
using UnityEngine.SceneManagement;

public class BGSceneFade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private const float FadeSpeed = 0.5f;
    private const string SceneUnityName = "SCN_Unity";
    private const string SceneTitleName = "SCN_Title";

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("BGSceneFade: Missing SpriteRenderer on the same GameObject.");
            enabled = false;
            return;
        }

        AudioSceneFading.ScreenFadeStatus = AudioSceneFading.FadeIn;
        AudioSceneFading.ScreenFadeAlpha = 1.0f;

        Color tmpColor = spriteRenderer.color;
        tmpColor.a = AudioSceneFading.ScreenFadeAlpha;
        spriteRenderer.color = tmpColor;
    }

    void Update()
    {
        if (AudioSceneFading.ScreenFadeStatus == AudioSceneFading.FadeIn)
        {
            AudioSceneFading.ScreenFadeAlpha -= FadeSpeed * Time.deltaTime;

            Color tmpColor = spriteRenderer.color;
            tmpColor.a = AudioSceneFading.ScreenFadeAlpha;
            spriteRenderer.color = tmpColor;

            if (AudioSceneFading.ScreenFadeAlpha <= 0.0f)
            {
                AudioSceneFading.ScreenFadeAlpha = 0.0f;
                AudioSceneFading.ScreenFadeStatus = AudioSceneFading.FadeNone;
            }
        }
        else if (AudioSceneFading.ScreenFadeStatus == AudioSceneFading.FadeOut)
        {
            AudioSceneFading.ScreenFadeAlpha += FadeSpeed * Time.deltaTime;

            Color tmpColorTwo = spriteRenderer.color;
            tmpColorTwo.a = AudioSceneFading.ScreenFadeAlpha;
            spriteRenderer.color = tmpColorTwo;

            if (AudioSceneFading.ScreenFadeAlpha >= 1.0f)
            {
                AudioSceneFading.ScreenFadeAlpha = 1.0f;
                AudioSceneFading.ScreenFadeStatus = AudioSceneFading.FadeNone;

                if (AudioSceneFading.NextSceneToDisplay == AudioSceneFading.SceneUnity)
                {
                    AudioSceneFading.PlayMusic(0, -1);
                    AudioSceneFading.CurrentSceneToDisplay = AudioSceneFading.SceneUnity;
                    AudioSceneFading.NextSceneToDisplay = AudioSceneFading.SceneTitle;
                    SceneManager.LoadScene(SceneUnityName);
                    return;
                }

                if (AudioSceneFading.NextSceneToDisplay == AudioSceneFading.SceneTitle)
                {
                    AudioSceneFading.CurrentSceneToDisplay = AudioSceneFading.SceneTitle;
                    AudioSceneFading.PlaySoundEffect(1, 0);
                    SceneManager.LoadScene(SceneTitleName);
                    return;
                }
            }
        }
    }
}

