using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CursorState
{
    Default,
    Pointer,
    Targeter
}

public class Cursor : MonoBehaviour
{
    public static Cursor Instance { get; private set; }

    [SerializeField] SerializableDictionary<CursorState, Sprite> cursorSprites;
    private Dictionary<CursorState, Sprite> spriteDict;

    public Vector3 WorldPosition { get; private set; }
    
    [SerializeField] SpriteRenderer sprite;
    Camera cam;

    void Awake()
    {
        Instance = this;

        spriteDict = cursorSprites.ToDict();
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 pointerPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        WorldPosition = pointerPos;
        pointerPos.z = -9;
        gameObject.transform.position = pointerPos;
    }

    public void SetCursorState(CursorState state)
    {
        sprite.sprite = spriteDict[state] ?? spriteDict[CursorState.Default];
    }
}
