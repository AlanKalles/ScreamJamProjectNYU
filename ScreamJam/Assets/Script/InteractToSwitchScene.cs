using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractToSwitchScene : Interactable
{
    public int sceneNumber;
    public bool changeStage;
    public float n;
    public override void Action()
    {
        SceneSwitch.SwitchToScene(sceneNumber);
        if (changeStage) { stageManager.instance.StartAndWait((GameStage)((int)stageManager.curStage + 1), n); }
    }
}
