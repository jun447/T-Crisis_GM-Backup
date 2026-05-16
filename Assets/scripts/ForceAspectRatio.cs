using UnityEngine;
using UnityEngine.Rendering;

public class ForceAspectRatio : MonoBehaviour
{
    public float targetAspectRatio = 16f / 9f;
    public new Camera camera;

    private CommandBuffer clearBuffer;

    void Start()
    {
        //Screen.SetResolution(640, 360, FullScreenMode.Windowed);
        camera = GetComponent<Camera>();
        SetCameraAspectRatio();

        clearBuffer = new CommandBuffer();
        clearBuffer.name = "ResizeClear";
        // Clear params: (clearDepth, clearColor, backgroundColor)
        clearBuffer.ClearRenderTarget(true, true, Color.black);
    }

    void Update()
    {
        SetCameraAspectRatio();
    }

    void OnPreRender()
    {
        // optional: Check if window resized this frame
        // and only if so execute the clear immediately before cameras draw
        // to avoid any performance impact during normal operations
        Graphics.ExecuteCommandBuffer(clearBuffer);
    }

    void SetCameraAspectRatio()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspectRatio;

        Rect rect = camera.rect;

        if (scaleHeight < 1.0f)
        {
            // Pillarbox (black bars on sides)
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        }
        else
        {
            // Letterbox (black bars top/bottom)
            float scaleWidth = 1.0f / scaleHeight;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
        }

        GL.Clear(true, true, Color.black); // Necessary to clear window on window resize on Linux Wayland

        camera.rect = rect;
    }
}   