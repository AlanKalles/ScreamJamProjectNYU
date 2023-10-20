using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Interactable
{
    public GameObject lockUI;
    private void Start()
    {
        quitable = true;
    }
    public override void Action()
    {
        lockUI.SetActive(true);
    }
    public override void quit()
    {
        lockUI.SetActive(false);
    }
}
