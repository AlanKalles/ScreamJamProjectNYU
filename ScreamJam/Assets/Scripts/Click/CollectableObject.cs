using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : Clickable
{
    public ObjectType type;
    internal override void OnClick()
    {
        InventoryManager.instance.AddInventory(GetComponent<SpriteRenderer>().sprite, transform, false);
        clickable = false;
    }
    public enum ObjectType
    {
        Mirror,
        Amulete
    }
}
