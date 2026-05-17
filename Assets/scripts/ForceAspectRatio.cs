using UnityEngine;
using UnityEngine.Rendering;

public class ForceAspectRatio : MonoBehaviour
{
    public float targetAspectRatio = 16f / 9f;
    [SerializeField] private Camera targetCamera;

    private int lastScreenWidth = -1;
    private int lastScreenHeight = -1;
    private bool needsClear;

    void Start()
    {
        //Screen.SetResolution(640, 360, FullScreenMode.Windowed);
        if (targetCamera == null)
        {
            targetCamera = GetComponent<Camera>();
        }
        if (targetCamera == null)
        {
            Debug.LogError("ForceAspectRatio: Missing Camera component on the same GameObject.");
            enabled = false;
            return;
        }
        SetCameraAspectRatio();
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        needsClear = true;
    }

    void Update()
    {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            SetCameraAspectRatio();
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            needsClear = true;
        }
    }

    void SetCameraAspectRatio()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspectRatio;

        Rect rect = targetCamera.rect;

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

        targetCamera.rect = rect;
    }

    void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
    }

    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
    }

    private void OnBeginCameraRendering(ScriptableRenderContext context, Camera cam)
    {
        if (!needsClear || cam != targetCamera)
        {
            return;
        }

        GL.Clear(true, true, Color.black); // Necessary to clear window on window resize on Linux Wayland
        needsClear = false;
    }
}   