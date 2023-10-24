using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : Clickable
{
    public ObjectType type;
    public bool changeStage;
    internal override void OnClick()
    {
        InventoryManager.instance.AddInventory(GetComponent<SpriteRenderer>().sprite, transform, false);
        clickable = false;
        if (changeStage)
        {
            stageManager.instance.StartAndWait((GameStage)((int)stageManager.curStage + 1), 0);
        }
    }
    public enum ObjectType
    {
        Other,
        Mirror,
        Incantation_Red,
        Incantation
    }
}
