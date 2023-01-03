using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Texture2D normalcursor, pressedcursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    void Start()
    {
        Cursor.SetCursor(normalcursor, hotSpot, cursorMode);
    }
    public void OnMouseEnterr()
    {
        Cursor.SetCursor(pressedcursor, hotSpot, cursorMode);
    }

    public void OnMouseExitt()
    {
        Cursor.SetCursor(normalcursor, Vector2.zero, cursorMode);
    }
}
