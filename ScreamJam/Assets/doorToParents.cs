using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class doorToParents : Interactable
{
    public int sceneNumber;
    public override void Action()
    {
        base.Action();
        SceneSwitch.SwitchToScene(sceneNumber);
    }

    internal override void OnTriggerEnter2D(Collider2D collision)
    {
        if(stageManager.curStage != GameStage.Day1ExploreHouse) { return; }
        base.OnTriggerEnter2D(collision);
    }
}
