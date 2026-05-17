using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class Continue : MonoBehaviour
{
    private const float UnitySceneWaitSeconds = 3.0f;
    private Coroutine waitCoroutine;

    private void OnEnable() => EnhancedTouchSupport.Enable();
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
        }
    }

    void Start()
    {
        if (AudioSceneFading.CurrentSceneToDisplay == AudioSceneFading.SceneUnity)
        {
            AudioSceneFading.ScreenDisplayTimer = UnitySceneWaitSeconds;
            waitCoroutine = StartCoroutine(WaitAndLoadScene());
        }
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(AudioSceneFading.ScreenDisplayTimer);

        AudioSceneFading.ScreenDisplayTimer = 0.0f;
        AudioSceneFading.ScreenFadeStatus = AudioSceneFading.FadeOut;
        waitCoroutine = null;
    }

    void Update()
    {
        var activeTouches = ETouch.Touch.activeTouches;
        foreach (var touch in activeTouches)
        {
            if (touch.began)
            {
                AudioSceneFading.ScreenFadeStatus = AudioSceneFading.FadeOut;
                AudioSceneFading.PlaySoundEffect(0, 0);
                return;
            }
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            AudioSceneFading.ScreenFadeStatus = AudioSceneFading.FadeOut;
            AudioSceneFading.PlaySoundEffect(0, 0);
            return;
        }
    }
}
