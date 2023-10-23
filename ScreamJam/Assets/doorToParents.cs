using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorToParents : Interactable
{
    public int sceneNumber;
    public override void Action()
    {
        if (stageManager.curStage != GameStage.Day1ExploreHouse) { return;  }
        SceneSwitch.SwitchToScene(sceneNumber);
        if (changeStage) { stageManager.instance.StartAndWait((GameStage)((int)stageManager.curStage + 1), n); }
    }
}
