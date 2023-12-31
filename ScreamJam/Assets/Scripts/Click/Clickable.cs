using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clickable : MonoBehaviour
{
    public Vector2 topRight, botLeft;
    public Vector3 hoverPosOffset;
    public bool checkStage;
    public GameStage targetStage;

    private static Vector2 mouseWorldPos;
    Vector3 originalPos, hoverPos;
    internal bool clickable = true;
    internal virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube((topRight + botLeft) / 2, topRight - botLeft);
    }
    internal virtual void Start()
    {
        mouseWorldPos = Vector2.zero;
        originalPos = transform.position;
        hoverPos = hoverPosOffset + originalPos;
    }
    internal virtual void FixedUpdate()
    {
        if (!clickable) return;
        if (checkStage && stageManager.curStage != targetStage) return;
        if (mouseWorldPos == Vector2.zero)
        {
            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (CheckBounds())
        {
            transform.position = hoverPos;
        }
        else
        {
            transform.position = originalPos;
        }
        mouseWorldPos = Vector2.zero;
    }
    internal virtual void Update()
    {
        if(!clickable) return;
        if (checkStage && stageManager.curStage != targetStage) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (mouseWorldPos == Vector2.zero)
                mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (CheckBounds())
                OnClick();
        }
    }
    private bool CheckBounds()
    {
        return mouseWorldPos.x > botLeft.x && mouseWorldPos.x < topRight.x && mouseWorldPos.y > botLeft.y && mouseWorldPos.y < topRight.y;
    }
    internal abstract void OnClick();
}
