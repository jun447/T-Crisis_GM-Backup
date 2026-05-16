using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class Continue : MonoBehaviour
{
    private void OnEnable() => EnhancedTouchSupport.Enable();
    private void OnDisable() => EnhancedTouchSupport.Disable();

    void Start()
    {
        if (AudioSceneFading.CurrentSceneToDisplay == AudioSceneFading.SceneUnity)
        {
            AudioSceneFading.ScreenDisplayTimer = 3.0f;
            StartCoroutine(WaitAndLoadScene());
        }
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(AudioSceneFading.ScreenDisplayTimer);

  	    AudioSceneFading.ScreenDisplayTimer = 0.0f;
  	    AudioSceneFading.ScreenFadeStatus = AudioSceneFading.FadeOut;
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

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            AudioSceneFading.ScreenFadeStatus = AudioSceneFading.FadeOut;
            AudioSceneFading.PlaySoundEffect(0, 0);
            return;
        }
    }
}
