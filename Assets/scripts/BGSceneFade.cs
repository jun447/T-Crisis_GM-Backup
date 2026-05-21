using UnityEngine;
using UnityEngine.SceneManagement;

public class BGSceneFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private const float FadeSpeed = 0.5f;
    private const string SceneUnityName = "SCN_Unity";
    private const string SceneTitleName = "SCN_Title";

    void Start()
    {
        if (canvasGroup == null)
        {
            Debug.LogError("BGSceneFade: Missing CanvasGroup assignment in the Inspector.");
            enabled = false;
            return;
        }

        AudioSceneFading.ScreenFadeStatus = AudioSceneFading.FadeIn;
        AudioSceneFading.ScreenFadeAlpha = 1.0f;

        canvasGroup.alpha = AudioSceneFading.ScreenFadeAlpha;
    }

    void Update()
    {
        if (AudioSceneFading.ScreenFadeStatus == AudioSceneFading.FadeIn)
        {
            AudioSceneFading.ScreenFadeAlpha -= FadeSpeed * Time.deltaTime;

            canvasGroup.alpha = AudioSceneFading.ScreenFadeAlpha;

            if (AudioSceneFading.ScreenFadeAlpha <= 0.0f)
            {
                AudioSceneFading.ScreenFadeAlpha = 0.0f;
                AudioSceneFading.ScreenFadeStatus = AudioSceneFading.FadeNone;
            }
        }
        else if (AudioSceneFading.ScreenFadeStatus == AudioSceneFading.FadeOut)
        {
            AudioSceneFading.ScreenFadeAlpha += FadeSpeed * Time.deltaTime;

            canvasGroup.alpha = AudioSceneFading.ScreenFadeAlpha;

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