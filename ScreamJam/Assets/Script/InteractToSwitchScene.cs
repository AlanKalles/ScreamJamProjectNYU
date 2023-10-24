using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractToSwitchScene : Interactable
{
    public int sceneNumber;
    public override void Action()
    {
        base.Action();
        SceneSwitch.SwitchToScene(sceneNumber);
    }
}
