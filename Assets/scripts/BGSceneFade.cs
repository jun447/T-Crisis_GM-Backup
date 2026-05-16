using UnityEngine;
using UnityEngine.SceneManagement;

public class BGSceneFade : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;

    void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();

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
			AudioSceneFading.ScreenFadeAlpha -= 0.5f * Time.deltaTime;

			Color tmpColor = spriteRenderer.color;
			tmpColor.a = AudioSceneFading.ScreenFadeAlpha;
			spriteRenderer.color = tmpColor;

			if (AudioSceneFading.ScreenFadeAlpha <= 0.0f)
			{
				AudioSceneFading.ScreenFadeAlpha = 0.0f;
				AudioSceneFading.ScreenFadeStatus = AudioSceneFading.FadeNone;
			}
		}

		if (AudioSceneFading.ScreenFadeStatus == AudioSceneFading.FadeOut)
		{
			AudioSceneFading.ScreenFadeAlpha += 0.5f * Time.deltaTime;

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
					SceneManager.LoadScene("SCN_Unity");
					return;
				}

				if (AudioSceneFading.NextSceneToDisplay == AudioSceneFading.SceneTitle)
				{
					AudioSceneFading.PlaySoundEffect(1, 0);
					AudioSceneFading.CurrentSceneToDisplay = AudioSceneFading.SceneTitle;
					SceneManager.LoadScene("SCN_Title");
					return;
				}
			}
		}
	}
}
