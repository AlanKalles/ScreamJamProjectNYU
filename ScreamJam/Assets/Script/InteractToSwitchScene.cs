using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractToSwitchScene : Interactable
{
    public int sceneNumber;
    public override void Action()
    {
        SceneSwitch.SwitchToScene(sceneNumber);
    }
}
