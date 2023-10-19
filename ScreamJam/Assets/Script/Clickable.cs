using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clickable : MonoBehaviour
{
    public Vector2 topRight, botLeft;
    public Vector3 hoverPosOffset;

    private static Vector2 mouseWorldPos;
    Vector3 originalPos, hoverPos;

    internal virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
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
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
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
