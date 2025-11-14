using UnityEngine;

public class MouseCursorController : MonoBehaviour
{
    [Tooltip("If true, the cursor will be enabled on awake, otherwise it will be disabled.")]
    public bool enableCursorAtStart = true;

    void Awake()
    {
        // Set the cursor visibility based on the flag
        Cursor.visible = enableCursorAtStart;
    }

    // Call this method to toggle the cursor visibility at any time
    public void ToggleCursor()
    {
        Cursor.visible = !Cursor.visible;
    }
}
