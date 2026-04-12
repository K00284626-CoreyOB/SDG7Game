using UnityEngine;
using UnityEngine.UI;

public class CleaningMinigameUI : MonoBehaviour
{
    public RawImage dirtImage;
    public RectTransform dirtRect;

    [Header("Source Art")]
    public Texture2D dirtyPanelTexture;

    public int brushRadius = 35;
    public float completionThreshold = 1.2f;

    private Texture2D runtimeTexture;
    private CleaningMinigameManager manager;
    private bool completed = false;

    public void Begin(CleaningMinigameManager newManager)
    {
        manager = newManager;
        completed = false;
        CreateFreshDirtTexture();
    }

    void CreateFreshDirtTexture()
    {
        if (dirtyPanelTexture == null)
        {
            Debug.LogError("dirtyPanelTexture is not assigned!");
            return;
        }

        runtimeTexture = new Texture2D(
            dirtyPanelTexture.width,
            dirtyPanelTexture.height,
            TextureFormat.RGBA32,
            false
        );

        runtimeTexture.SetPixels(dirtyPanelTexture.GetPixels());
        runtimeTexture.Apply();

        dirtImage.texture = runtimeTexture;
    }

    void Update()
    {
        if (runtimeTexture == null)
            return;

#if UNITY_EDITOR
        HandleMouse();
#else
        HandleTouch();
#endif
    }

    void HandleMouse()
    {
        if (Input.GetMouseButton(0))
        {
            TryEraseAtScreenPoint(Input.mousePosition);
        }
    }

    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began ||
                touch.phase == TouchPhase.Moved ||
                touch.phase == TouchPhase.Stationary)
            {
                TryEraseAtScreenPoint(touch.position);
            }
        }
    }

    void TryEraseAtScreenPoint(Vector2 screenPos)
    {
        if (runtimeTexture == null || dirtRect == null)
            return;

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dirtRect, screenPos, null, out localPoint))
        {
            Rect rect = dirtRect.rect;

            float normalizedX = Mathf.InverseLerp(rect.xMin, rect.xMax, localPoint.x);
            float normalizedY = Mathf.InverseLerp(rect.yMin, rect.yMax, localPoint.y);

            int px = Mathf.RoundToInt(normalizedX * runtimeTexture.width);
            int py = Mathf.RoundToInt(normalizedY * runtimeTexture.height);

            EraseCircle(px, py);

            if (!completed && GetCleanPercent() >= completionThreshold)
            {
                completed = true;

                if (manager != null)
                {
                    manager.CompleteMinigame();
                }
            }
        }
    }

    void EraseCircle(int centerX, int centerY)
    {
        if (runtimeTexture == null)
            return;

        for (int y = -brushRadius; y <= brushRadius; y++)
        {
            for (int x = -brushRadius; x <= brushRadius; x++)
            {
                if (x * x + y * y > brushRadius * brushRadius)
                    continue;

                int px = centerX + x;
                int py = centerY + y;

                if (px < 0 || px >= runtimeTexture.width || py < 0 || py >= runtimeTexture.height)
                    continue;

                Color c = runtimeTexture.GetPixel(px, py);
                c.a = 0f;
                runtimeTexture.SetPixel(px, py, c);
            }
        }

        runtimeTexture.Apply();
    }

    float GetCleanPercent()
    {
        if (runtimeTexture == null)
            return 0f;

        Color[] pixels = runtimeTexture.GetPixels();
        int erasedCount = 0;

        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].a <= 0.05f)
                erasedCount++;
        }

        return (float)erasedCount / pixels.Length;
    }
}