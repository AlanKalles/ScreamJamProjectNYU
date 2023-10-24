using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceReceiver : MonoBehaviour
{
    public Vector2 topRight, botLeft;
    public CollectableObject.ObjectType receivingType;

    public static List<PieceReceiver> receivers;
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube((topRight + botLeft) / 2, topRight - botLeft);
    }
    // Start is called before the first frame update
    internal virtual void Start()
    {
        if(receivers == null)
        {
            receivers = new List<PieceReceiver>();
        }
        receivers.Add(this);
    }
    public bool CheckGameObject(GameObject go)
    {
        CollectableObject co=go.GetComponent<CollectableObject>();
        if (co == null)
            return false;
        return CompareType(co.type);
    }
    public bool CheckBounds(Vector2 mouseWorldPos)
    {
        return mouseWorldPos.x > botLeft.x && mouseWorldPos.x < topRight.x && mouseWorldPos.y < botLeft.y && mouseWorldPos.y > topRight.y;
    }
    public abstract void Action(GameObject go);
    internal virtual bool CompareType(CollectableObject.ObjectType type)
    {
        return type == receivingType;
    }
}
