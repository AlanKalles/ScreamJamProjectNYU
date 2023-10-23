using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorWithStageLock : Interactable
{
    public int sceneNumber;
    public GameStage tarStage;
    public override void Action()
    {

        SceneSwitch.SwitchToScene(sceneNumber);
        if (changeStage) { stageManager.instance.StartAndWait((GameStage)((int)stageManager.curStage + 1), n); }
    }

    internal override void OnTriggerEnter2D(Collider2D collision)
    {
        if (stageManager.curStage != tarStage) { return; }
        base.OnTriggerEnter2D(collision);
    }
}
