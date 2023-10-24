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
            // �����Ʒ������ȷ�������ƶ����̶�λ�ò�ִ�������������
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
