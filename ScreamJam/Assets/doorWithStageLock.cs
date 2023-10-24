using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorWithStageLock : Interactable
{
    public int sceneNumber;
    public GameStage tarStage,toStage;
    public override void Action()
    {
        SceneSwitch.SwitchToScene(sceneNumber);
        base.Action();
    }

    internal override void OnTriggerEnter2D(Collider2D collision)
    {
        int targetFrom = (int)tarStage, targetTo=(int)toStage;
        if (targetTo <= targetFrom && tarStage == stageManager.curStage)
        {
            base.OnTriggerEnter2D(collision);
            return;
        }
        int curStage = (int)stageManager.curStage;
        if (curStage >= targetFrom && curStage <= targetTo)
            base.OnTriggerEnter2D(collision);
    }
}
