using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorReceiver : PieceReceiver {
    public Transform door;
    public override void Action(GameObject go)
    {
        if (CompareType(go.GetComponent<CollectableObject>().type))
        {
            // 如果物品类型正确，则将其移动到固定位置并执行其他所需操作
            GameObject clone = Instantiate(go, door.position, Quaternion.identity);
            InventoryManager.instance.DeselectInventory();
            stageManager.instance.CurChallengeResult = stageManager.challengeResult.success;
        }
        else
        {
            Debug.Log("Wrong item type!");
        }
    }

    
}
